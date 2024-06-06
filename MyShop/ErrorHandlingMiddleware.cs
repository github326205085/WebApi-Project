﻿using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MyShop;
using Services;
using System.Threading.Tasks;
namespace MyShop;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, ILogger<ErrorHandlingMiddleware> _logger)
       { 
        try{
            await _next(httpContext);
         }

        catch(Exception e)
        {
            _logger.LogInformation($"Logged From My Middleware {e.Message}  {e.StackTrace}");
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsync("Internal Error In Server");
        }

    }


}


// Extension method used to add the middleware to the HTTP request pipeline.
public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}

