using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PizzaCuDeToateAPI;
using PizzaCuDeToateAPI.DataContexts;
using PizzaCuDeToateAPI.Repositories.CategoryRepository;
using PizzaCuDeToateAPI.Repositories.FoodItemRepository;
using PizzaCuDeToateAPI.Repositories.StockItemRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddControllers().AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddTransient<DbContext, ApplicationContext>();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PizzaCuDeToate_Db")));

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

app.Seed();
app.Run();