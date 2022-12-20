using Microsoft.OpenApi.Models;
using ReadFileService.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Bvr.CreditLimit.Service",
    });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
});
services.AddControllers();
services.AddHealthChecks();
services.AddScoped<IProcessFileDataService, ProcessFileDataService>();
var app = builder.Build();

//Swagger

#region swagger

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Bvr.CreditLimit.Service");
});

#endregion swagger

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();