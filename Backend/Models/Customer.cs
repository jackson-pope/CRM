using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Emails = new HashSet<Email>();
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? Address4 { get; set; }
        public string? Address5 { get; set; }
        public string? Country { get; set; }
        public string? Acquisition { get; set; }
        public string? KickstarterUsername { get; set; }
        public string? Comment { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
