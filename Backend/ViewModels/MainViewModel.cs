using Microsoft.Toolkit.Mvvm.ComponentModel;

using Backend.Models;


namespace Backend.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private CrmService _service;
        private List<CustomerOverview> _customers;

        public List<CustomerOverview> Customers { get => _customers; private set => SetProperty(ref _customers, value); }

        public MainViewModel()
        {
            _service = new CrmService(new CrmContext());
            _customers = new List<CustomerOverview>();

            Customers = _service.GetAllCustomers().Select(c => new CustomerOverview(c)).ToList();
        }
    }
}
