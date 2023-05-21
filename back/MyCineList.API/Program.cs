using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyCineList.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "MyCineList", Version = "v1"});
});

builder.WebHost.UseIISIntegration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyCineList"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(option => option.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin());

app.MapControllers();

app.Run();
