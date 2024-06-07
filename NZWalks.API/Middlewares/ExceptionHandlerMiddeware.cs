using System.Net;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddeware
    {
        private readonly ILogger<ExceptionHandlerMiddeware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddeware(ILogger<ExceptionHandlerMiddeware> logger,
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                var errorId = Guid.NewGuid();

                // Log this exception
                _logger.LogError(e, $"{errorId}: {e.Message}");

                // Return a custom Exrror Reponse
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong! We are looking into resolving this",
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
