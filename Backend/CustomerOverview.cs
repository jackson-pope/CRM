using Backend.Models;

namespace Backend
{
    public class CustomerOverview
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Country { get; private set; }
        public string EmailAddress { get; private set; }
        public decimal LTV { get; private set; }
        public string LtvDisplay { get; }

        public CustomerOverview(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            Id = customer.Id;
            Name = customer.Name ?? string.Empty;
            Country = customer.Country ?? string.Empty;
            EmailAddress = customer.Emails.First().EmailAddress;
            LTV = customer.Invoices.Sum(i => i.InvoiceTotal);
            LtvDisplay = LTV == 0M ? "-" : LTV.ToString("C");
        }
    }
}
