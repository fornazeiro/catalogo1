using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Connections.Features;
using System.Net;
using System.Runtime.CompilerServices;

namespace CatalogoAPI.Extensions;

public static class ApiExtensionsMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFature != null)
                {
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFature.Error.Message,
                        Trace = contextFature.Error.StackTrace
                    }.ToString());
                }
            });
        });
    }
}
