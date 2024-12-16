

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//enable routing
app.UseRouting();

//creating endpoints
app.UseEndpoints(endpoints =>
{
   
    //sales-report/2024/jan
    endpoints.Map("sales-report/{year}/{month}", async context =>
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        
            await context.Response.WriteAsync($"sales report - {year} - {month}");
        
    });

    //sales-report/2024/jan
    endpoints.Map("sales-report/2024/jan", async context =>
    {
        await context.Response.WriteAsync("Sales report exclusively for 2024 - jan");
    });
});

app.Run(async context => {
    await context.Response.WriteAsync($"No route matched at {context.Request.Path}");
});
app.Run();