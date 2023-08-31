using ExcelData.WebApi.Configurations;
using ExcelData.WebApi.Configurations.Layers;
using ExcelData.WebApi.Middlewares;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

builder.ConfigureCORSPolicy();
builder.ConfigureDataAcces();
builder.ConfigureServiceLayer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseMiddleware<ExceptionHandlerMiddlewares>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
