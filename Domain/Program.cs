
using Core.Interfaces;
using Core.Interfaces.Auth;
using Core.Services.Auth;
using Infrastructure.Content.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IngredientsService>();
builder.Services.AddScoped<OrdersService>();
builder.Services.AddScoped<IPizzas, PizzaService>();
builder.Services.AddScoped<PizzaIngredientsService>();
builder.Services.AddScoped<StaffService>();
builder.Services.AddScoped<ReviewsService>();
builder.Services.AddScoped<OrderItemsService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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