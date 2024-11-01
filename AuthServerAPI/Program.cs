using Microsoft.Extensions.DependencyInjection;
using Services;
using Shared;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();
builder.Services.Configure<CustomTokenOption>(app.Configuration.GetSection("TokenOption"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Console.WriteLine(Assembly.GetExecutingAssembly());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
