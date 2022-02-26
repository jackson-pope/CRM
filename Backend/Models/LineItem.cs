using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class LineItem
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public int Invoice { get; set; }
        public string Product { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual Invoice InvoiceNavigation { get; set; } = null!;
        public virtual Product ProductNavigation { get; set; } = null!;
    }
}
