using Microsoft.AspNetCore.Builder;
using Middleware.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public static class ServiceRegistration
    {
        public static void RegisterMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
