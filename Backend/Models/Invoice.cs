using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            LineItems = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public int Customer { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string InvoiceNumber { get; set; } = null!;
        public string Reference { get; set; } = null!;
        public string? Note { get; set; }
        public int? KickstarterBacker { get; set; }
        public string? GamefoundOrder { get; set; }
        public decimal InvoiceTotal { get; set; }
        public decimal? KickstarterTotal { get; set; }

        public virtual Customer CustomerNavigation { get; set; } = null!;
        public virtual PaymentType PaymentMethodNavigation { get; set; } = null!;
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
