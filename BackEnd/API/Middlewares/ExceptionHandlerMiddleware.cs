using API.Exceptions;
using System.Net;
using System.Text.Json;

namespace API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;


        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "Ocorreu um erro interno inesperado.";

 
            switch (exception)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;
     

            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;


            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
   
            };

 
            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
