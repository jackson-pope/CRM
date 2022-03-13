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

        public CustomerViewModel(Customer c)
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
        }
    }
}
