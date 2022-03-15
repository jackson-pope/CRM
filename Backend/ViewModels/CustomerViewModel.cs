using Backend.Models;

namespace Backend.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? CountryCode { get; set; }
        public string? Acquisition { get; set; }
        public string EmailAddresses { get; set; }
        public string Products { get; set; }

        public CustomerViewModel(Customer customer, List<Product> allProducts)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            Id = customer.Id;
            Name = customer.Name;
            PhoneNumber = customer.PhoneNumber;
            Acquisition = customer.Acquisition;
            CountryCode = customer.Country;
            EmailAddresses = string.Join('\n', customer.Emails.Select(e => e.EmailAddress));
            var address = new string?[] { customer.Address1, customer.Address2, customer.Address3, customer.Address4, customer.Address5 };
            Address = string.Join('\n', address.Where(s => !string.IsNullOrEmpty(s)));
            var products = customer.Invoices.SelectMany(i => i.LineItems)
                                            .GroupBy(l => l.Product)
                                            .Select(g => new LineItem { Product = g.First().Product, Quantity = g.Sum(l => l.Quantity) })
                                            .OrderBy(l => l.Product)
                                            .Select(l => string.Format("{0}x {1}", l.Quantity, allProducts.First(p => p.Sku == l.Product).Description));
            Products = string.Join('\n', products);
        }
    }
}
