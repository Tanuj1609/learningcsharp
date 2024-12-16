var builder = WebApplication.CreateBuilder(args);

//adding all the controller classes as services
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
