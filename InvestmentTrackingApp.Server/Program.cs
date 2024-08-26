using InvestmentTracking.Business.Services;
using InvestmentTracking.Business.Services.Interfaces;
using InvestmentTracking.Data.Model;
using InvestmentTracking.Data.Repositories;
using InvestmentTracking.Data.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICachingPurchaseLotRepository, CachingPurchaseLotRepository>();
builder.Services.AddTransient<IInvestmentCalculator, InvestmentCalculator>();
//builder.Services.AddScoped<IInvestmentCalculator, InvestmentCalculator>();

builder.Services.AddMemoryCache();

var app = builder.Build();

// Preload cache with data on application startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var cache = services.GetRequiredService<IMemoryCache>();

    // Preload cache with PurchaseLots data
    var purchaseLots = new List<PurchaseLot>
    {
        new PurchaseLot(100, 20m, new DateTime(2024, 1, 1)), 
        new PurchaseLot(150, 30m, new DateTime(2024, 2, 1)), 
        new PurchaseLot(120, 10m, new DateTime(2024, 3, 1)) 
    };

    var cacheEntryOptions = new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(10))
        .SetAbsoluteExpiration(TimeSpan.FromHours(1));

    cache.Set("PurchaseLots", purchaseLots, cacheEntryOptions);
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
