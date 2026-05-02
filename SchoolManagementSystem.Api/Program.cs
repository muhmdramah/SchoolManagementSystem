#region Namespaces
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Infrastructure;
using SchoolManagementSystem.Infrastructure.Context;
using SchoolManagementSystem.Service;
#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

#region SQL Server Configuartions
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region Services Dependency Injection 
builder.Services.AddInfrastructureDependencies()
    .AddServiceDependencies();
#endregion

#region Caching Configuration
builder.Services.AddOutputCache(options =>
{
    options.DefaultExpirationTimeSpan = TimeSpan.FromMinutes(10); // Cache responses for 10 minutes by default
    options.MaximumBodySize = 1024 * 1024; // Cache responses up to 1 MB in size
    options.SizeLimit = 100 * 1024 * 1024; // Set the total cache size limit to 100 MB
    options.UseCaseSensitivePaths = false; // Treat paths as case-insensitive for caching

    options.AddPolicy("CacheSingleStudentResponse", policy =>
    {
        policy.SetVaryByRouteValue(["id"]) // Vary cache by the "id" route parameter
              .Expire(TimeSpan.FromSeconds(10)); // Cache responses for 10 seconds

        policy.Tag(["single-student"]);
    });
});
#endregion


var app = builder.Build();

#region Swagger and OpenApi
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management System API V1");
        c.RoutePrefix = "swagger";
    });
    app.MapOpenApi();
}
#endregion

// Configure the HTTP request pipeline.

#region Middlwares
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseOutputCache();

app.MapControllers();
#endregion

app.Run();
