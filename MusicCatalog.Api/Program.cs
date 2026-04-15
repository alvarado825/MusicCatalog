using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using MusicCatalog.Api.Filters;
using MusicCatalog.Api.Middlewares;
using MusicCatalog.Application;
using MusicCatalog.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Registro do Filter
builder.Services.AddControllers(options =>
{
    options.Filters.Add<RequestLoggingFilter>();
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Registro Persistencia
builder.Services.AddInfrastructureDependences(builder.Configuration);
//Registro Use Cases
builder.Services.AddApplicationDependences();
//Versionamento de Api
builder.Services.AddApiVersioning(options =>
{
   options.DefaultApiVersion = new ApiVersion(1,0);
   options.AssumeDefaultVersionWhenUnspecified = false;
   options.ReportApiVersions = true;
   options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

//Mapear Controllers
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services
            .GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant()
            );
        }
    });
    
}


app.UseHttpsRedirection();

app.Run();
