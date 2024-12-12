using custommiddlewareclass;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<custommiddlewareClass>();
var app = builder.Build();

//middleware 1 
app.Use(async(context, next) =>{
    await context.Response.WriteAsync("Hello this is middleware 1\n");
    await next(context);
});


//middleware 2
app.custommiddlewareClass();

//middleware 3
app.Run(async (context) => {
    await context.Response.WriteAsync("Hello this is middleware 3\n");
});

app.Run();
  