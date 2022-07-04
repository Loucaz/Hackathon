using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDb>(opt => opt.UseInMemoryDatabase("ProductList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/products", async (ProductDb db) =>
    await db.Products.ToListAsync());

app.MapGet("/products/categori/{categori}", async (string categori, ProductDb db) =>
    await db.Products.Where(t => t.categori == categori).ToListAsync());

app.MapPost("/products", async (Product product, ProductDb db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();

    return Results.Created($"/products/{product.Id}", product);
});

app.MapGet("/products/{id}", async (int id, ProductDb db) =>
    await db.Products.FindAsync(id)
        is Product product
            ? Results.Ok(product)
            : Results.NotFound());


app.MapPut("/products/{id}", async (int id, Product inputProduct, ProductDb db) =>
{
    var product = await db.Products.FindAsync(id);

    if (product is null) return Results.NotFound();

    product.name = inputProduct.name;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/products/{id}", async (int id, ProductDb db) =>
{
    if (await db.Products.FindAsync(id) is Product product)
    {
        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return Results.Ok(product);
    }

    return Results.NotFound();
});

app.Run();

public class Product
{

    public int Id { get; set; }
    public string? brandName { get; set; }
    public string? name { get; set; }
    public string? imageUrl { get; set; }
    public string? categori { get; set; }
}
class ProductDb : DbContext
{
    public ProductDb(DbContextOptions<ProductDb> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}

