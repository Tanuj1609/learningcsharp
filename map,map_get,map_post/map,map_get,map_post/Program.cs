var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.Map("map1", async (context) =>
{
    await context.Response.WriteAsync("U r in map1");
});

    endpoints.Map("map2", async (context) =>
    {
        await context.Response.WriteAsync("U r in map2");
    });
});


app.Run();