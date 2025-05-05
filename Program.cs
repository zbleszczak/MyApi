using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Konfiguracja połączenia z bazą danych
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Konfiguracja CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

// Dodaj Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Użyj CORS
app.UseCors("AllowAll");

// Mapowanie endpointów API
app.MapGet("/api/products", async (MyDbContext db) => await db.Products.ToListAsync());
app.MapGet("/api/products/{id}", async (int id, MyDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    return product is null ? Results.NotFound() : Results.Ok(product);
});
app.MapPost("/api/products", async (Product product, MyDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/api/products/{product.Id}", product);
});
app.MapPut("/api/products/{id}", async (int id, Product updatedProduct, MyDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();
    
    product.Name = updatedProduct.Name;
    product.Brand = updatedProduct.Brand;
    product.Size = updatedProduct.Size;
    product.Price = updatedProduct.Price;
    
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/api/products/{id}", async (int id, MyDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();
    
    db.Products.Remove(product);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Uruchom Razor Pages
app.MapRazorPages();

app.Run();
