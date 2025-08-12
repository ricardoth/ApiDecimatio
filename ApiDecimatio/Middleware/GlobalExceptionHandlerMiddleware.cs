using Decimatio.Domain.Exceptions;

namespace Decimatio.WebApi.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            ErrorResponse errorResponse = new()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = exception.Message
            };

            switch (exception)
            {
                case NotFoundException notFoundException:
                    errorResponse.Message = notFoundException.Message;
                    errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case BadRequestException badRequestException:
                    errorResponse.Message = badRequestException.Message;
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case ValidationResultException validationResultException:
                    errorResponse.Message = validationResultException.Message;
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case NoContentException noContentException:
                    errorResponse.Message = noContentException.Message;
                    errorResponse.StatusCode = (int)HttpStatusCode.NoContent;
                    context.Response.StatusCode = (int)HttpStatusCode.NoContent;
                    break;

                default:
                    _logger.LogError(exception, "Ha ocurrido un error inesperado en la API");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
        }
    }
}
