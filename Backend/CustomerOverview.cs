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

        public CustomerOverview(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            Country = customer.Country;
            EmailAddress = customer.EmailAddress;
            LTV = customer.Invoices.Sum(i => i.InvoiceTotal);
        }
    }
}
