var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWhen(context => context.Request.Query.ContainsKey("Username"),
    app =>
    {
        app.Use(async (context, next)=>
        {
            await context.Response.WriteAsync("Username is found in query");
            await next();
        });
    });


app.Run(async context =>
{
    await context.Response.WriteAsync("Username is not found in query");
});

app.Run();
