using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public string Category1 { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
