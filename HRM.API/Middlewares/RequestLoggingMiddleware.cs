using HRM.API.Domain.DTOs;
using HRM.API.Domain.Entities;
using HRM.API.Infrastructure.Data;
using HRM.API.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace HRM.API.Middlewares // Tùy thuộc namespace của bạn
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ConnectionStrings _conn;

        public RequestLoggingMiddleware(RequestDelegate next, IOptions<ConnectionStrings> conn)
        {
            _next = next;
            _conn = conn.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var log = new LogEntity
            {
                Timestamp = DateTime.UtcNow
            };

            // Ghi log request
            await LogRequest(context, log);

            // Thay thế Response.Body bằng MemoryStream để ghi log
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                // Gọi middleware tiếp theo trong pipeline
                await _next(context);

                // Đảm bảo con trỏ ở đầu trước khi ghi log
                responseBody.Seek(0, SeekOrigin.Begin);
                await LogResponse(context, responseBody, log);

                // Debug để kiểm tra độ dài
                Console.WriteLine($"Response length before copy: {responseBody.Length}");

                // Sao chép nội dung từ MemoryStream về originalBodyStream
                responseBody.Seek(0, SeekOrigin.Begin); // Đặt lại con trỏ trước khi sao chép
                await responseBody.CopyToAsync(originalBodyStream);

                // Debug để xác nhận dữ liệu đã được sao chép
                Console.WriteLine($"Response copied to original stream");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in middleware: {ex.Message}");
                throw; // Ném lỗi để xử lý ở cấp cao hơn nếu cần
            }
            finally
            {
                // Khôi phục Response.Body ban đầu
                context.Response.Body = originalBodyStream;
            }
        }

        private async Task LogRequest(HttpContext context, LogEntity log)
        {
            context.Request.EnableBuffering();
            var requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8)
                .ReadToEndAsync();
            context.Request.Body.Position = 0;

            log.Method = context.Request.Method;
            log.Path = context.Request.Path;
            log.QueryString = context.Request.QueryString.ToString();
            log.RequestBody = requestBody;

            try
            {
                using var publisher = new SqlserverProvider().Init(_conn.DefaultConnection ?? "");
                publisher.Logs.Add(log);
                await publisher.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log request: {ex.Message}");
            }
        }

        private async Task LogResponse(HttpContext context, MemoryStream responseBody, LogEntity log)
        {
            var responseContent = await new StreamReader(responseBody, Encoding.UTF8)
                .ReadToEndAsync();

            log.StatusCode = context.Response.StatusCode;
            log.ResponseBody = responseContent;

            try
            {
                using var publisher = new SqlserverProvider().Init(_conn.DefaultConnection ?? "");
                publisher.Entry(log).State = EntityState.Modified;
                await publisher.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log response: {ex.Message}");
            }
        }
    }
}

public static class RequestLoggingMiddlewareeExtensions
{
    public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}


public class SqlserverProvider
{
    public ApplicationDbContext Init(string connectionString)
    {
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly("Petnimal.API");
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
            });
        return new ApplicationDbContext(contextOptions.Options);
    }
}