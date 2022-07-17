using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public static class InvoicesController
    {
	public static void MapInvoiceEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Invoice", async (ProductsDbContext db) =>
        {
            return await db.Invoices.ToListAsync();
        })
        .WithName("GetAllInvoices");

        routes.MapGet("/api/Invoice/{id}", async (int Id, ProductsDbContext db) =>
        {
            return await db.Invoices.FindAsync(Id)
                is Invoice model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetInvoiceById");

        routes.MapPut("/api/Invoice/{id}", async (int Id, Invoice invoice, ProductsDbContext db) =>
        {
            var foundModel = await db.Invoices.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            //update model properties here

            await db.SaveChangesAsync();

            return Results.NoContent();
        })   
        .WithName("UpdateInvoice");

        routes.MapPost("/api/Invoice/", async (Invoice invoice, ProductsDbContext db) =>
        {
            db.Invoices.Add(invoice);
            await db.SaveChangesAsync();
            return Results.Created($"/Invoices/{invoice.Id}", invoice);
        })
        .WithName("CreateInvoice");


        routes.MapDelete("/api/Invoice/{id}", async (int Id, ProductsDbContext db) =>
        {
            if (await db.Invoices.FindAsync(Id) is Invoice invoice)
            {
                db.Invoices.Remove(invoice);
                await db.SaveChangesAsync();
                return Results.Ok(invoice);
            }

            return Results.NotFound();
        })
        .WithName("DeleteInvoice");
    }
    }
}
