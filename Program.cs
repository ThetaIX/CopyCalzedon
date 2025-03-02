using Microsoft.AspNetCore.Builder; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CalzedoniaHRFeed.Services;


var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер DI
builder.Services.AddControllers();
builder.Services.AddSingleton<PersonProcessingService>();

// Настройка Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Person Upload API",
        Version = "v1",
        Description = "API для загрузки персональных данных"
    });
});

var app = builder.Build();

// Настройка middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Person Upload API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();