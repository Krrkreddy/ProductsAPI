using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public static class ProductsController
    {
	public static void MapProductEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Product", async (ProductsDbContext db) =>
        {
            return await db.Products.ToListAsync();
        })
        .WithName("GetAllProducts");

        routes.MapGet("/api/Product/{id}", async (int Id, ProductsDbContext db) =>
        {
            return await db.Products.FindAsync(Id)
                is Product model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetProductById");

        routes.MapPut("/api/Product/{id}", async (int Id, Product product, ProductsDbContext db) =>
        {
            var foundModel = await db.Products.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            //update model properties here

            await db.SaveChangesAsync();

            return Results.NoContent();
        })   
        .WithName("UpdateProduct");

        routes.MapPost("/api/Product/", async (Product product, ProductsDbContext db) =>
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/Products/{product.Id}", product);
        })
        .WithName("CreateProduct");


        routes.MapDelete("/api/Product/{id}", async (int Id, ProductsDbContext db) =>
        {
            if (await db.Products.FindAsync(Id) is Product product)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return Results.Ok(product);
            }

            return Results.NotFound();
        })
        .WithName("DeleteProduct");
    }
    }
}
