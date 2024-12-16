var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//enable routing
app.UseRouting();

//creating endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.Map("files/{filename}.{extension}", async (context) =>
    {
    string? filename = Convert.ToString(context.Request.RouteValues["filename"]);
    string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
    await context.Response.WriteAsync($"In files - {filename} - {extension}");
});


    endpoints.Map("employee/{employeename}", async (context) =>
    {
        string? employeename = Convert.ToString(context.Request.RouteValues["employeename"]);
       
        await context.Response.WriteAsync($"In emplouyee section - {employeename}");
    });
});

app.Run(async context => {
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();