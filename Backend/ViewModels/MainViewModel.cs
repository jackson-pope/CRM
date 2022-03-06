using Microsoft.Toolkit.Mvvm.ComponentModel;

using Backend.Models;

namespace Backend.ViewModels
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }


    public class MainViewModel : ObservableObject
    {
        private CrmService _service;
        private List<CustomerOverview> _allCustomers;

        public string? SortedColumn { get; private set; }
        public SortDirection SortedDirection { get; private set; }

        private string _filter = string.Empty;
        public string? Filter { get => _filter; set { FilterCustomers(value); } }

        private List<CustomerOverview> _customers;
        public List<CustomerOverview> Customers { get => _customers; private set => SetProperty(ref _customers, value); }

        public MainViewModel(IServiceProvider provider)
        {
            _service = new CrmService((CrmContext)provider.GetService(typeof(CrmContext)));
            _allCustomers = _service.GetAllCustomers().Select(c => new CustomerOverview(c)).ToList();

            SortCustomers("LTV", SortDirection.Descending);
        }
        public void SortCustomers(string columnName, SortDirection direction)
        {
            SortedColumn = columnName;
            SortedDirection = direction;

            BuildCustomerList();
        }

        public void FilterCustomers(string filter)
        {
            SetProperty(ref _filter, filter);

            BuildCustomerList();
        }

        public void BuildCustomerList()
        {
            var filtered = _allCustomers.Where(c => c.Name.Contains(_filter) || c.EmailAddress.Contains(_filter));
            IEnumerable<CustomerOverview> sorted = Enumerable.Empty<CustomerOverview>();
            switch (SortedColumn)
            {
                case "Name":
                    if (SortedDirection == SortDirection.Ascending)
                        sorted = filtered.OrderBy(c => c.Name);
                    else
                        sorted = filtered.OrderByDescending(c => c.Name);
                    break;
                case "Email":
                    if (SortedDirection == SortDirection.Ascending)
                        sorted = filtered.OrderBy(c => c.EmailAddress);
                    else
                        sorted = filtered.OrderByDescending(c => c.EmailAddress);
                    break;
                case "Country":
                    if (SortedDirection == SortDirection.Ascending)
                        sorted = filtered.OrderBy(c => c.Country);
                    else
                        sorted = filtered.OrderByDescending(c => c.Country);
                    break;
                case "LTV":
                    if (SortedDirection == SortDirection.Ascending)
                        sorted = filtered.OrderBy(c => c.LTV);
                    else
                        sorted = filtered.OrderByDescending(c => c.LTV);
                    break;
                default:
                    break;
            }
            Customers = sorted.ToList();
        }
   }
}
