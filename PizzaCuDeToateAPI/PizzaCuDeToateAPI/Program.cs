using System.Configuration;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaCuDeToateAPI;
using PizzaCuDeToateAPI.DataContexts;
using PizzaCuDeToateAPI.Models;
using PizzaCuDeToateAPI.Repositories.CategoryRepository;
using PizzaCuDeToateAPI.Repositories.FoodItemRepository;
using PizzaCuDeToateAPI.Repositories.StockItemRepository;
using PizzaCuDeToateAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();

//Add entity dbContext for app, option to stop looping over cyclic reference data, add postgresql connection for dbContext
builder.Services.AddTransient<DbContext, ApplicationContext>();
builder.Services.AddControllers().AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PizzaCuDeToate_Db")));

//Add identity service
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
    }
).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

//Add authentication with jwt (later)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});
builder.Services.AddScoped<IJWTService, JWTService>();

//Add email configuration
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailService, EmailService>();

//Add provider configuration for frontend
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
builder.Services.AddCors(options =>
{
    var frontendUrl = configuration.GetValue<string>("Frontend_Url");
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontendUrl).AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Seed(builder.Configuration.GetConnectionString("PizzaCuDeToate_Db"));
app.Run();