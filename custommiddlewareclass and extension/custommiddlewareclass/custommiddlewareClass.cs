
namespace custommiddlewareclass
{
    public class custommiddlewareClass : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("middlwware starts \n");
            await context.Response.WriteAsync("in the middle of middleware\n");
            await context.Response.WriteAsync("end of middleware class\n");
            await next(context);
        }
    }
}


public static class custommiddleware
{
    public static IApplicationBuilder custommiddlewareClass(this IApplicationBuilder app)
    {
        return app.UseMiddleware<custommiddlewareclass>();
    }
}
