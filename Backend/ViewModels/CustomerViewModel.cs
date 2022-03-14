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

        public CustomerViewModel(Customer c, List<Product> allProducts)
        {
            if (c == null)
                return;

            Id = c.Id;
            Name = c.Name;
            PhoneNumber = c.PhoneNumber;
            Acquisition = c.Acquisition;
            CountryCode = c.Country;
            EmailAddresses = string.Join('\n', c.Emails.Select(e => e.EmailAddress));
            var address = new string[] { c.Address1, c.Address2, c.Address3, c.Address4, c.Address5 };
            Address = string.Join('\n', address.Where(s => !string.IsNullOrEmpty(s)));
            var products = c.Invoices.SelectMany(i => i.LineItems)
                                     .GroupBy(l => l.Product)
                                     .Select(g => new LineItem { Product = g.First().Product, Quantity = g.Sum(l => l.Quantity) })
                                     .OrderBy(l => l.Product)
                                     .Select(l => string.Format("{0}x {1}", l.Quantity, allProducts.First(p => p.Sku == l.Product).Description));
            Products = string.Join('\n', products);
        }
    }
}
