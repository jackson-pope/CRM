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

            SortData("LTV", SortDirection.Descending);
        }

        public void SortData(string columnName, SortDirection direction)
        {
            var input = _service.GetAllCustomers().Select(c => new CustomerOverview(c));
            var sorted = Enumerable.Empty<CustomerOverview>();
            switch (columnName)
            {
                case "Name":
                    CachedSortedColumn = "Name";
                    if (direction == SortDirection.Ascending)
                        sorted = input.OrderBy(c => c.Name);
                    else
                        sorted = input.OrderByDescending(c => c.Name);
                    break;
                case "Email":
                    CachedSortedColumn = "Email";
                    if (direction == SortDirection.Ascending)
                        sorted = input.OrderBy(c => c.EmailAddress);
                    else
                        sorted = input.OrderByDescending(c => c.EmailAddress);
                    break;
                case "Country":
                    CachedSortedColumn = "Country";
                    if (direction == SortDirection.Ascending)
                        sorted = input.OrderBy(c => c.Country);
                    else
                        sorted = input.OrderByDescending(c => c.Country);
                    break;
                case "LTV":
                    CachedSortedColumn = "LTV";
                    if (direction == SortDirection.Ascending)
                        sorted = input.OrderBy(c => c.LTV);
                    else
                        sorted = input.OrderByDescending(c => c.LTV);
                    break;
                default:
                    break;
            }
            Customers = sorted.ToList();
        }
    }
}
