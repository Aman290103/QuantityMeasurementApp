using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuantityMeasurementApp.WebApi.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            var error = "Internal Server Error";
            var message = exception.Message;

            if (exception is QuantityMeasurementException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                error = "Quantity Measurement Error";
            }
            else if (exception is ArgumentException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                error = "Bad Request";
            }

            var errorMessage = exception.InnerException != null 
                ? $"{exception.Message} | Inner Error: {exception.InnerException.Message}" 
                : exception.Message;

            var problemDetails = new
            {
                timestamp = DateTime.UtcNow.ToString("O"),
                status = statusCode,
                error = error,
                message = errorMessage,
                path = httpContext.Request.Path.Value
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            
            return true;
        }
    }
}
