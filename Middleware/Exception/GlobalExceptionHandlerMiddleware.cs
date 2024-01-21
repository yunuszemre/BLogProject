using Microsoft.AspNetCore.Http;


namespace Middleware.Exception
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Source);

            }
        }
    }

}
