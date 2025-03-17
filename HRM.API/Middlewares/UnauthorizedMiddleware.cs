using HRM.API.Domain.DTOs;
using HRM.API.Utils.Constants;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class UnauthorizedMiddleware
{
    private readonly RequestDelegate _next;

    public UnauthorizedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
        {

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Áp dụng camelCase
            };
            var response = ApiResponse<String>.Error(MessageErrorConstants.Unauthorize);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }
}

// Mở rộng phương thức middleware
public static class UnauthorizedMiddlewareExtensions
{
    public static IApplicationBuilder UseUnauthorizedMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UnauthorizedMiddleware>();
    }
}
