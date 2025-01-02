using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Add(new ServiceDescriptor(
  typeof(ICitiesService),
  typeof(CitiesService),
  ServiceLifetime.Scoped));

//Below is the shortcut method for implementing the transient service
builder.Services.AddTransient<ICitiesService, CitiesService>();
//Below is the shortcut method for implementing the Scoped service
builder.Services.AddScoped<ICitiesService, CitiesService>();
//Below is the shortcut method for implementing the Singleton service
builder.Services.AddSingleton<ICitiesService, CitiesService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();