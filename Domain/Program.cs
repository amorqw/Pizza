
using System.Text;
using Core;
using Core.Interfaces;
using Core.Interfaces.Auth;
using Core.Interfaces.Jwt;
using Infrastructure.Content.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Pizza.Services;
using Pizza.Services.Auth;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<OrdersService>();
builder.Services.AddScoped<IPizzas, PizzaService>();
builder.Services.AddScoped<IIngredients, IngredientsService>();
builder.Services.AddScoped<PizzaIngredientsService>();
builder.Services.AddScoped<StaffService>();
builder.Services.AddScoped<PizzaService>();
builder.Services.AddScoped<ReviewsService>();
builder.Services.AddScoped<IUser, UserService>();  
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

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HomeController}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
