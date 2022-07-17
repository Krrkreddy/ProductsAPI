using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public static class ProductCategoriesController
    {
	public static void MapProductCategoryEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/ProductCategory", async (ProductsDbContext db) =>
        {
            return await db.ProductCategories.ToListAsync();
        })
        .WithName("GetAllProductCategorys");

        routes.MapGet("/api/ProductCategory/{id}", async (int Id, ProductsDbContext db) =>
        {
            return await db.ProductCategories.FindAsync(Id)
                is ProductCategory model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetProductCategoryById");

        routes.MapPut("/api/ProductCategory/{id}", async (int Id, ProductCategory productCategory, ProductsDbContext db) =>
        {
            var foundModel = await db.ProductCategories.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            //update model properties here

            await db.SaveChangesAsync();

            return Results.NoContent();
        })   
        .WithName("UpdateProductCategory");

        routes.MapPost("/api/ProductCategory/", async (ProductCategory productCategory, ProductsDbContext db) =>
        {
            db.ProductCategories.Add(productCategory);
            await db.SaveChangesAsync();
            return Results.Created($"/ProductCategorys/{productCategory.Id}", productCategory);
        })
        .WithName("CreateProductCategory");


        routes.MapDelete("/api/ProductCategory/{id}", async (int Id, ProductsDbContext db) =>
        {
            if (await db.ProductCategories.FindAsync(Id) is ProductCategory productCategory)
            {
                db.ProductCategories.Remove(productCategory);
                await db.SaveChangesAsync();
                return Results.Ok(productCategory);
            }

            return Results.NotFound();
        })
        .WithName("DeleteProductCategory");
    }
        
    }
}
