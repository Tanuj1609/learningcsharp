using CitiesManager.WebAPI.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(new ProducesAttribute("applications/json"))); //NOW BY DEFAULT THE RESPONSE BODY CONTENT TYPE OF ALL ACTION METHODS IS APPLICATIONS JSON
builder.Services.AddControllers(options => options.Filters.Add(new ConsumesAttribute("applications/json"))).AddXmlSerializerFormatters(); //NOW BY DEFAULT THE REQUEST BODY CONTENT TYPE OF ALL ACTION METHODS IS APPLICATIONS JSON
builder.Services.AddApiVersioning(config => { config.ApiVersionReader = new UrlSegmentApiVersionReader(); //URL SEGMENT API VERSION READER EXTRACTS THE API VERSION FROM THE VERSION URL PATH
                                            //  config.ApiVersionReader = new QueryStringApiVersionReader();
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;

}); //QUERY STRING API VERSION READER EXTRACTS THE API VERSION FROM THE REQUEST QUERY STRING "api-version"
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


//USING SWAGGER 
builder.Services.AddEndpointsApiExplorer(); // GENERATES DESCRIPTION FOR ALL END POINTS
builder.Services.AddSwaggerGen(options => { 
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml")); // GENERATES OPEN API SPECIFICATION
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() {Title = "Cities web api", Version = "1.0"}); //configures Swagger to generate a documentation file for an API named "Cities web api" with version "1.0"
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Cities web api", Version = "2.0" });
});

builder.Services.AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHsts();
app.UseHttpsRedirection();
app.UseSwagger(); //Creates endpoint for swagger.json
app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
});  // creates swagger ui for testing all web api endpoints/action methods
app.UseAuthorization();

app.MapControllers();

app.Run();
