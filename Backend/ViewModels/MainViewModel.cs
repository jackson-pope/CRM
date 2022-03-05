using Microsoft.Toolkit.Mvvm.ComponentModel;

using Backend.Models;
using System.Collections;

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
        private List<CustomerOverview> _customers;
        public string? CachedSortedColumn { get; private set; }

        public List<CustomerOverview> Customers { get => _customers; private set => SetProperty(ref _customers, value); }

        public MainViewModel(IServiceProvider provider)
        {
            _service = new CrmService((CrmContext)provider.GetService(typeof(CrmContext)));
            _customers = new List<CustomerOverview>();

            Customers = _service.GetAllCustomers().Select(c => new CustomerOverview(c)).ToList();
        }

        public void SortData(string columnName, SortDirection direction)
        {
            var input = _service.GetAllCustomers().Select(c => new CustomerOverview(c));
            switch (columnName)
            {
                case "Name":
                    if (direction == SortDirection.Ascending)
                        Customers = input.OrderBy(c => c.Name).ToList();
                    else
                        Customers = input.OrderByDescending(c => c.Name).ToList();
                    break;
                case "Email":
                    if (direction == SortDirection.Ascending)
                        Customers = input.OrderBy(c => c.EmailAddress).ToList();
                    else
                        Customers = input.OrderByDescending(c => c.EmailAddress).ToList();
                    break;
                case "Country":
                    if (direction == SortDirection.Ascending)
                        Customers = input.OrderBy(c => c.Country).ToList();
                    else
                        Customers = input.OrderByDescending(c => c.Country).ToList();
                    break;
                default:
                    Customers = new List<CustomerOverview>();
                    break;
            }
        }
    }
}
