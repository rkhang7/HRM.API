

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


using HRM.API.Infrastructure.Data;
using HRM.API.Domain.Interfaces;
using HRM.API.Domain.Entities;
using HRM.API.Infrastructure.Repositories;
using HRM.API.Application.Commands;
using HRM.API.Domain.DTOs.CreateCommomDto;
using HRM.API.Domain.DTOs;
using MediatR;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICommomRepository, CommonRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();


// Configure MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));





// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));




// Configure JWT Authentication
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//        };
//    });
//builder.Services.AddAuthorization();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HR Management API",
        Version = "v1",
        Description = "API for managing HR",

    });
    //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    In = ParameterLocation.Header,
    //    Description = "Please enter JWT with Bearer into field (e.g., 'Bearer {token}')",
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.ApiKey,
    //    Scheme = "Bearer"
    //});
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type = ReferenceType.SecurityScheme,
    //                Id = "Bearer"
    //            }
    //        },
    //        Array.Empty<string>()
    //    }
    //});
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management API V1");
        c.RoutePrefix = string.Empty;
        c.DocumentTitle = "Library API Documentation";
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();