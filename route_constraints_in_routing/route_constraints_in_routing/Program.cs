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


    endpoints.Map("employee/{employeename=THANOS}", async (context) =>
    {
        string? employeename = Convert.ToString(context.Request.RouteValues["employeename"]);

        await context.Response.WriteAsync($"In employee section - {employeename}");
    });

    endpoints.Map("product/detail/{id:int?}", async (context) =>
    {
        if (context.Request.RouteValues.ContainsKey("id"))
        {
            int id = Convert.ToInt32(context.Request.RouteValues["id"]);

            await context.Response.WriteAsync($"Product id is - {id}");
        }

        else
        {
            await context.Response.WriteAsync($"Product id is not supplied");
        }
    });

    endpoints.Map("daily-report/{report_date:datetime}", async (context) =>
    {
        DateTime reportdate = Convert.ToDateTime(context.Request.RouteValues["report_date"]);
        await context.Response.WriteAsync($"The date reported by you is: {reportdate.ToShortDateString()}");
    });
});

app.Run(async context => {
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();