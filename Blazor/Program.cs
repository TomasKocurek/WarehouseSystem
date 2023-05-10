using Blazor.Services;
using Havit.Blazor.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add services
builder.Services.AddScoped<ApiClient>();
builder.Services.AddScoped<ProductsService>();
builder.Services.AddScoped<StockItemsService>();
builder.Services.AddScoped<StocksService>();
builder.Services.AddScoped<MovementsService>();
builder.Services.AddScoped<OrdersService>();

// Add havit components
builder.Services.AddHxServices();
builder.Services.AddHxMessageBoxHost();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
