using InvestmentTracking.Business.Services;
using InvestmentTracking.Business.Services.Interfaces;
using InvestmentTracking.Data.Repositories;
using InvestmentTracking.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPurchaseLotRepository,PurchaseLotRepository>();
builder.Services.AddSingleton<ICachingPurchaseLotRepository, CachingPurchaseLotRepository>();
builder.Services.AddTransient<IInvestmentCalculator, InvestmentCalculator>();
//builder.Services.AddScoped<IInvestmentCalculator, InvestmentCalculator>();

builder.Services.AddMemoryCache();

var app = builder.Build();

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
