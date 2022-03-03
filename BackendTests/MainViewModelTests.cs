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

            Assert.That(vm.Customers[0].Name, Is.EqualTo("Archie"));
            Assert.That(vm.Customers[1].Name, Is.EqualTo("Bertie"));
            Assert.That(vm.Customers[2].Name, Is.EqualTo("Chuck"));
        }

        [Test]
        public void Sort_WhenSortingOnNameDescending_SortsCorrectly()
        {

            var vm = new MainViewModel(_provider);
            vm.SortData("Name", SortDirection.Descending);

            Assert.That(vm.Customers[0].Name, Is.EqualTo("Chuck"));
            Assert.That(vm.Customers[1].Name, Is.EqualTo("Bertie"));
            Assert.That(vm.Customers[2].Name, Is.EqualTo("Archie"));
        }

        [Test]
        public void Sort_WhenSortingOnEmailAscending_SortsCorrectly()
        {

            var vm = new MainViewModel(_provider);
            vm.SortData("Email", SortDirection.Ascending);

            Assert.That(vm.Customers[0].EmailAddress, Is.EqualTo("bertie@example.com"));
            Assert.That(vm.Customers[1].EmailAddress, Is.EqualTo("c.overview@example.com"));
            Assert.That(vm.Customers[2].EmailAddress, Is.EqualTo("overview.archie@example.com"));
        }

        [Test]
        public void Sort_WhenSortingOnEmailDescending_SortsCorrectly()
        {

            var vm = new MainViewModel(_provider);
            vm.SortData("Email", SortDirection.Descending);

            Assert.That(vm.Customers[0].EmailAddress, Is.EqualTo("overview.archie@example.com"));
            Assert.That(vm.Customers[1].EmailAddress, Is.EqualTo("c.overview@example.com"));
            Assert.That(vm.Customers[2].EmailAddress, Is.EqualTo("bertie@example.com"));
        }
    }
}
