using API.Exceptions;
using System.ComponentModel.DataAnnotations;
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
            string titulo = "Ocorreu um erro inesperado.";
            string detalhes = exception.Message;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    titulo = "Recurso não encontrado.";
                    break;

                //case BadRequestException:
                //    statusCode = HttpStatusCode.BadRequest;
                //    title = "Requisição inválida.";
                //    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    titulo = "Acesso não autorizado.";
                    break;

                //case ForbiddenException:
                //    statusCode = HttpStatusCode.Forbidden;
                //    title = "Acesso negado.";
                //    break;

                //case ConflictException:
                //    statusCode = HttpStatusCode.Conflict;
                //    title = "Conflito de dados.";
                //    break;

                case ValidationException:
                    statusCode = HttpStatusCode.UnprocessableEntity;
                    titulo = "Erro de validação.";
                    break;
            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;

            var erroDetalhes = new
            {
                type = $"https://httpstatuses.com/{(int)statusCode}",
                titulo,
                status = (int)statusCode,
                detalhes,
                instance = context.Request.Path
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(erroDetalhes, jsonOptions));
        }
    }
}
