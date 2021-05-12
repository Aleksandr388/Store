using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic;
using Store.Presentation.Middlewares.Providers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace Store.Presentation.Middlewares
{
    public class LoggingErrorsMiddleware
    {
        private RequestDelegate _next;

        public ILoggerFactory _loggerFactory;

        public LoggingErrorsMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _loggerFactory = loggerFactory;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (CustomExeption customExeption)
            {
                string jsonString = JsonSerializer.Serialize(customExeption.Error);
                context.Response.StatusCode = customExeption.StatusCode;
                await context.Response.WriteAsync(jsonString);
            }
            catch (Exception exeption)
            {
                _loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
                var logger = _loggerFactory.CreateLogger("logger.txt");
                logger.LogError($"{DateTime.Now}, {exeption.Message}, {exeption.StackTrace}");
            }
        }
    }
}
