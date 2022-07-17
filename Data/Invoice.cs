using System;
using System.Collections.Generic;

namespace ProductsAPI.Data
{
    public partial class Invoice
    {
        public int Id { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }

        public virtual Product? Product { get; set; }
    }
}
