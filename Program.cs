
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connString = builder.Configuration.GetConnectionString("ProductsDB");
builder.Services.AddDbContext<ProductsDbContext>(options => options.UseSqlServer(connString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddDbContext<ProductsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductsDB"));

});*/

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

app.MapProductEndpoints();

app.MapProductCategoryEndpoints();

app.MapInvoiceEndpoints();

app.Run();
