using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Product
    {
        public Product()
        {
            LineItems = new HashSet<LineItem>();
        }

        public string Sku { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal? KsPrice { get; set; }
        public decimal? WebPrice { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Depth { get; set; }
        public int? Weight { get; set; }
        public decimal? Cogs { get; set; }

        public virtual Category CategoryNavigation { get; set; } = null!;
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
