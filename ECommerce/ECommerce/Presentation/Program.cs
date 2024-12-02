using ECommerce.Business;
using ECommerce.DataAccess;
using ECommerce.Factorymethod;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(cfg => {
//    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(x => {
//    x.RequireHttpsMetadata = false;
//    x.SaveToken = true;
//    x.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8
//            .GetBytes(builder.Configuration["Jwt:Key"])
//        ),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ClockSkew = TimeSpan.Zero
//    };
//});


// Add ECommerceContext to DI container
builder.Services.AddDbContext<ECommerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services for Customer
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ICustomerFactory, CustomerFactory>();
builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();

// Register services for Order
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<IOrderFactory, OrderFactory>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();

// Register services for Product
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IProductFactory, ProductFactory>();
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();

// Register services for OrderItem
builder.Services.AddScoped<OrderItemService>(); // Register OrderItemService
builder.Services.AddScoped<IOrderItemFactory, OrderItemFactory>(); // Register factory for OrderItem
builder.Services.AddScoped<IRepository<OrderItem>, OrderItemRepository>(); // Register repository for OrderItem

// Add FluentValidation and register all validators
builder.Services.AddValidatorsFromAssemblyContaining<CustomerValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OrderValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<OrderItemValidator>(); // Register OrderItemValidator

// Add CustomerDtoValidator
builder.Services.AddScoped<IValidator<CustomerDto>, CustomerDtoValidator>();

//builder.Services.AddAuthorization();

// Add FluentValidation to the MVC pipeline
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CustomerValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<OrderValidator>();
        //fv.RegisterValidatorsFromAssemblyContaining<OrderItemValidator>(); // Register OrderItem validators
    });

builder.Services.AddSwaggerGen();
//builder.Services.AddAuthorization();

//builder.Services.AddSwaggerGen(c =>
//{
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.Http,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: 'Bearer 12345abcdef'"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] { }
//        }
//    });
//});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
