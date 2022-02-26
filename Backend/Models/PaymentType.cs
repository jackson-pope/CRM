using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Invoices = new HashSet<Invoice>();
        }

        public string Type { get; set; } = null!;

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
