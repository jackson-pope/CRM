using Microsoft.Toolkit.Mvvm.ComponentModel;

using Backend.Models;


namespace Backend.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private CrmContext _context;
        private List<CustomerOverview> _customers;

        public List<CustomerOverview> Customers { get => _customers; private set => SetProperty(ref _customers, value); }

        public MainViewModel()
        {
            _context = new CrmContext();
            _customers = new List<CustomerOverview>();

            Customers = _context.Customers.Select(c => new CustomerOverview(c)).ToList();
        }
    }
}
