using Domain_Layer.Exceptions;
using Shared.ErrorModels;

namespace E_Commerce.Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next,ILogger<CustomExceptionHandlerMiddleWare> Logger)
        {
            _next = Next;
            _logger = Logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                await HandleNotFoundEndPointAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Occurs");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new ErrorToReturn()
            {
                ErrorMessage = ex.Message,
            };

            // Set Status Code For Response
            response.StatusCode = ex switch
            {
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestExceptions badRequestExceptions => GetBadRequestErrors(badRequestExceptions,response),
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = response.StatusCode;

            // Return Object As JSON
            await context.Response.WriteAsJsonAsync(response);
        }


        private static async Task HandleNotFoundEndPointAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {context.Request.Path} is Not Found"
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
        private static int GetBadRequestErrors(BadRequestExceptions badRequestExceptions, ErrorToReturn response)
        {
            response.Errors = badRequestExceptions.Errors;

            return StatusCodes.Status400BadRequest;
        }
    }
}
