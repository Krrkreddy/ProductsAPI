using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;

namespace ProductsAPI.Data
{
    public partial class Product
    {
        public Product()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Cost { get; set; }

        public virtual ProductCategory? Category { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
	
    }
}
