var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//routing enabled
app.UseRouting();

//endpoints defining
app.UseEndpoints(endpoints =>
{
    //we can define the end points here
});
app.Run();
