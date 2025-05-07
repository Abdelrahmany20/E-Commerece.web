using Domain.Exeptions;
using Shared.ErrorModule;
using System.Text.Json;

namespace E_Commerece.web.CustomMiddleWares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionMiddleware> logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);

                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {

                
                    var response = new ErrorToReturn()
                    {
                        StatusCode = httpContext.Response.StatusCode,
                        ErrorMessage = $"End Point {httpContext.Request.Path} Is Not Found"
                    };
                    var responseToReturn = JsonSerializer.Serialize(response);
                    await httpContext.Response.WriteAsync(responseToReturn);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Something Went Wrong");

                var response = new ErrorToReturn()
                {
                    ErrorMessage = ex.Message,
                    

                };


                response.StatusCode  = ex switch
                {
                    NotFoundExecption => StatusCodes.Status404NotFound,
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    BadRequestExeption badRequestExeption => GetBadRequestErrors(badRequestExeption, response) ,
                    _ => StatusCodes.Status500InternalServerError
                }  ;


                
                httpContext.Response.ContentType = "application/json";




                var responseToReturn = JsonSerializer.Serialize(response);



                await httpContext.Response.WriteAsync(responseToReturn);
            }
        }


        private static int GetBadRequestErrors(BadRequestExeption badRequestExeption,ErrorToReturn response)
        {
            response  .Errors =badRequestExeption.Errors;
            return StatusCodes.Status400BadRequest;


        }
    }
}
