
using System.Text;
using Core;
using Core.Interfaces;
using Core.Interfaces.Auth;
using Core.Interfaces.Jwt;
using Core.Models;
using Infrastructure.Content.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Pizza.Services;
using Pizza.Services.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Session";
    options.IdleTimeout = TimeSpan.FromMinutes(120); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtOptions:Issuer"],  
            ValidAudience = builder.Configuration["JwtOptions:Audience"],  
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"])) 
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["tasty-cookies"];
                return Task.CompletedTask;
            }

        };
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});



builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IOrders,OrdersService>();
builder.Services.AddScoped<IPizzas, PizzaService>();
builder.Services.AddScoped<PizzaIngredientsService>();
builder.Services.AddScoped<StaffService>();
builder.Services.AddScoped<IPizzasAvailable, PizzasAvailableService>();
builder.Services.AddScoped<IOrderItems, OrderItemsService>();
builder.Services.AddScoped<PizzaService>();
builder.Services.AddScoped<IStaff, StaffService>();
builder.Services.AddScoped<IPizzeria, PizzeriaService>();
builder.Services.AddScoped<IReviews,RewiewService>();
builder.Services.AddScoped<IUser, UserService>(); 
builder.Services.AddScoped<IReviews, RewiewService>(); 
builder.Services.AddScoped<OrderItemsService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddControllers();

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = "swagger";  
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HomeController}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
