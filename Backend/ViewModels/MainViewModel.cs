using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.UI;
using Microsoft.UI.Xaml.Media;

using Backend.Models;
using System.Text;
using Microsoft.UI.Xaml.Input;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace Backend.ViewModels
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }


    public class MainViewModel : ObservableObject
    {
        private readonly CrmService _service;
        private readonly List<CustomerOverview> _allCustomers;

        public string? SortedColumn { get; private set; }
        public SortDirection SortedDirection { get; private set; }

        private string _filter = string.Empty;
        public string? Filter { get => _filter; set { FilterCustomers(value); } }

        private ICommand _combineCustomersCommand;
        public ICommand CombineCustomersCommand { get => _combineCustomersCommand; }

        private CustomerOverview? _selectedOverview;
        public CustomerOverview? SelectedOverview
        {
            get => _selectedOverview;
            set
            {
                SelectCustomer(value);
            }
        }

        private CustomerViewModel? _selectedCustomer;
        public CustomerViewModel? SelectedCustomer { get => _selectedCustomer; }

        private IEnumerable<CustomerOverview> _selectedCustomers;
        public IEnumerable<CustomerOverview> SelectedCustomers { get => _selectedCustomers; }

        private List<CustomerOverview> _customers;
        public List<CustomerOverview> Customers { get => _customers; private set => SetProperty(ref _customers, value); }

        public MainViewModel(IServiceProvider provider)
        {
            var context = (CrmContext?)provider.GetService(typeof(CrmContext));
            if (context == null)
                throw new Exception("Cannot find a CrmContext in the service provider");

            _service = new CrmService(context);
            _allCustomers = _service.GetAllCustomers().Select(c => new CustomerOverview(c)).ToList();
            _customers = new List<CustomerOverview>();

            SortCustomers("LTV", SortDirection.Descending);

            _combineCustomersCommand = new RelayCommand(new Action(CombineCustomers), CanCombineCustomers);

            _selectedCustomers = Enumerable.Empty<CustomerOverview>();
        }

        private bool CanCombineCustomers()
        {
            return _selectedCustomers.Count() > 1;
        }

        private void CombineCustomers()
        {
            throw new NotImplementedException();
        }

        public void SortCustomers(string columnName, SortDirection direction)
        {
            SortedColumn = columnName;
            SortedDirection = direction;

            BuildCustomerList();
        }

        public void FilterCustomers(string? filter)
        {
            SetProperty(ref _filter, filter ?? string.Empty);

            BuildCustomerList();
        }

        public void BuildCustomerList()
        {
            var filtered = _allCustomers.Where(c => c.Name.Contains(_filter) || c.EmailAddress.Contains(_filter));
            IEnumerable<CustomerOverview> sorted;
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
                    sorted = filtered;
                    break;
            }
            Customers = sorted.ToList();
        }

        public void SelectCustomers(IEnumerable<CustomerOverview> selected)
        {
            _selectedCustomers = selected;
        }

        public void SelectCustomer(CustomerOverview? selected)
        {
            SetProperty(ref _selectedOverview, selected, nameof(SelectedOverview));
            var customer = _service.GetAllCustomers().FirstOrDefault(c => c.Id == (selected != null ? selected.Id : -1));
            SetProperty(ref _selectedCustomer, (customer != null ? new CustomerViewModel(customer, _service.GetAllProducts()) : null), nameof(SelectedCustomer));
        }

        public string Export()
        {
            var builder = new StringBuilder();

            foreach (var customer in Customers)
            {
                builder.Append(String.Format("{0}, {1}\n", customer.EmailAddress, customer.LTV));
            }

            return builder.ToString();
        }
    }
}
