using Microsoft.AspNetCore.Builder;
using MinimalApi.Dtos;
using MinimalApi.Middlewares;
using MinimalApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddSingleton<ITodoService, TodoService>();
builder.Services.AddScoped<LogActionFilter>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestTimingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseRouting();

app.MapGet("/minimal/todos", (ITodoService svc) => svc.GetAll());


app.MapPost("/minimal/todos", (TodoCreateDto dto, ITodoService svc) => {
    var item = svc.Create(dto.Title, dto.IsCompleted);
    return Results.Created($"/minimal/todos/{item.Id}", item);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
