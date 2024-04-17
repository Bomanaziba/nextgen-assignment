using System.Text.Json.Serialization;
using Mapster;
using PaySpace.Calculator.API.Middleware;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddMapster();

builder.Services.AddCalculatorServices();
builder.Services.AddDataServices(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseMiddleware<ApplicationExcpetionHandler>();
app.InitializeDatabase();


app.Run();