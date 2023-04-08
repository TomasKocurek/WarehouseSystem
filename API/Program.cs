using API.Services;
using API.Services.DispatchProductService;
using Infrastructure.Extensions;
using MediatR;
using Shared.Mapping;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//add automapper
builder.Services.AddAutoMapper(
    typeof(MovementProfile),
    typeof(ProductProfile),
    typeof(StockItemProfile),
    typeof(StockProfile),
    typeof(OrderProfile));

builder.Services.AddScoped<StockSuggestionService>();
builder.Services.AddScoped<ABCService>();
builder.Services.AddScoped<DispatchProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
