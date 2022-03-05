using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Moq;
using NUnit.Framework;

using Backend;
using Backend.Models;
using Backend.ViewModels;

namespace BackendTests
{
    [TestFixture]
    public class MainViewModelTests
    {
        private IServiceProvider _provider;

        public MainViewModelTests()
        {
            var data = new List<Customer>
                            {
                                new Customer { Name = "Archie", EmailAddress = "overview.archie@example.com", Country="UK", Invoices = new List<Invoice> { new Invoice { InvoiceTotal = 10M } } },
                                new Customer { Name = "Bertie", EmailAddress = "bertie@example.com", Country = "France", Invoices = new List<Invoice>() },
                                new Customer { Name = "Chuck", EmailAddress = "c.overview@example.com", Country = "Belgium", Invoices = new List<Invoice> { new Invoice { InvoiceTotal = 20M } } }
                            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CrmContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);

            var services = new ServiceCollection();
            services.AddSingleton<CrmContext>(mockContext.Object);
            _provider = services.BuildServiceProvider();
        }

        [Test]
        public void Sort_WhenSortingOnNameAscending_SortsCorrectly()
        {

            var vm = new MainViewModel(_provider);
            vm.SortData("Name", SortDirection.Ascending);

            CollectionAssert.AreEqual(new string[] { "Archie", "Bertie", "Chuck"}, vm.Customers.Select(c => c.Name));
        }

        [Test]
        public void Sort_WhenSortingOnNameDescending_SortsCorrectly()
        {

            var vm = new MainViewModel(_provider);
            vm.SortData("Name", SortDirection.Descending);

            CollectionAssert.AreEqual(new string[] { "Chuck", "Bertie", "Archie" }, vm.Customers.Select(c => c.Name));
        }

        [Test]
        public void Sort_WhenSortingOnEmailAscending_SortsCorrectly()
        {

            var vm = new MainViewModel(_provider);
            vm.SortData("Email", SortDirection.Ascending);

            CollectionAssert.AreEqual(new string[] { "bertie@example.com", "c.overview@example.com", "overview.archie@example.com" }, vm.Customers.Select(c => c.EmailAddress));
        }

        [Test]
        public void Sort_WhenSortingOnEmailDescending_SortsCorrectly()
        {

            var vm = new MainViewModel(_provider);
            vm.SortData("Email", SortDirection.Descending);

            CollectionAssert.AreEqual(new string[] { "overview.archie@example.com", "c.overview@example.com", "bertie@example.com" }, vm.Customers.Select(c => c.EmailAddress));
        }

        [Test]
        public void Sort_WhenSortingOnCountry_SortsCorrectly()
        {

            var vm = new MainViewModel(_provider);
            vm.SortData("Country", SortDirection.Ascending);

            CollectionAssert.AreEqual(new string[] { "Belgium", "France", "UK" }, vm.Customers.Select(c => c.Country));
        }

        [Test]
        public void Sort_WhenSortingOnCountryDescending_SortsCorrectly()
        {

            var vm = new MainViewModel(_provider);
            vm.SortData("Country", SortDirection.Descending);

            CollectionAssert.AreEqual(new string[] { "UK", "France", "Belgium" }, vm.Customers.Select(c => c.Country));
        }
    }
}
