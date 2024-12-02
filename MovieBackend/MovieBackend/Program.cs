using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Default Connection String with Trusted Certificate
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    "Server=localhost;Database=MovieDatabase;Trusted_Connection=True;TrustServerCertificate=True;";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500") // Ýzin verilen origin'ler
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger Configuration (Optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
