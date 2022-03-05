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
        public void Constructor_SetsCachedSortedColumn_ToLTV()
        {
            // Arrange & Act
            var vm = new MainViewModel(_provider);

            // Assert
            Assert.That(vm.CachedSortedColumn, Is.EqualTo("LTV"));
        }

        [Test]
        public void Sort_WhenSortingOnNameAscending_SortsCorrectly()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("Name", SortDirection.Ascending);

            // Assert
            CollectionAssert.AreEqual(new string[] { "Archie", "Bertie", "Chuck"}, vm.Customers.Select(c => c.Name));
        }

        [Test]
        public void Sort_WhenSortingOnNameDescending_SortsCorrectly()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("Name", SortDirection.Descending);

            // Assert
            CollectionAssert.AreEqual(new string[] { "Chuck", "Bertie", "Archie" }, vm.Customers.Select(c => c.Name));
        }

        [Test]
        public void Sort_WhenSortingOnEmailAscending_SortsCorrectly()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("Email", SortDirection.Ascending);

            // Assert
            CollectionAssert.AreEqual(new string[] { "bertie@example.com", "c.overview@example.com", "overview.archie@example.com" }, vm.Customers.Select(c => c.EmailAddress));
        }

        [Test]
        public void Sort_WhenSortingOnEmailDescending_SortsCorrectly()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("Email", SortDirection.Descending);

            // Assert
            CollectionAssert.AreEqual(new string[] { "overview.archie@example.com", "c.overview@example.com", "bertie@example.com" }, vm.Customers.Select(c => c.EmailAddress));
        }

        [Test]
        public void Sort_WhenSortingOnCountry_SortsCorrectly()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("Country", SortDirection.Ascending);

            // Assert
            CollectionAssert.AreEqual(new string[] { "Belgium", "France", "UK" }, vm.Customers.Select(c => c.Country));
        }

        [Test]
        public void Sort_WhenSortingOnCountryDescending_SortsCorrectly()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("Country", SortDirection.Descending);

            // Assert
            CollectionAssert.AreEqual(new string[] { "UK", "France", "Belgium" }, vm.Customers.Select(c => c.Country));
        }

        [Test]
        public void Sort_WhenSortingOnLtv_SortsCorrectly()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("LTV", SortDirection.Ascending);

            // Assert
            CollectionAssert.AreEqual(new decimal[] { 0.0M, 10.0M, 20.0M }, vm.Customers.Select(c => c.LTV));
        }

        [Test]
        public void Sort_WhenSortingOnLtvDescending_SortsCorrectly()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("LTV", SortDirection.Descending);

            // Assert
            CollectionAssert.AreEqual(new decimal[] { 20.0M, 10.0M, 0.0M }, vm.Customers.Select(c => c.LTV));
        }


        [Test]
        public void Sort_WhenSortingOnNameDescending_SetsCachedSortedColumn()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("Name", SortDirection.Descending);

            // Assert
            Assert.That(vm.CachedSortedColumn, Is.EqualTo("Name"));
        }

        [Test]
        public void Sort_WhenSortingOnEmailAscending_SetsCachedSortedColumn()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("Email", SortDirection.Ascending);

            // Assert
            Assert.That(vm.CachedSortedColumn, Is.EqualTo("Email"));
        }

        [Test]
        public void Sort_WhenSortingOnEmailDescending_SetsCachedSortedColumn()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("Email", SortDirection.Descending);

            // Assert
            Assert.That(vm.CachedSortedColumn, Is.EqualTo("Email"));
        }

        [Test]
        public void Sort_WhenSortingOnCountry_SetsCachedSortedColumn()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("Country", SortDirection.Ascending);

            // Assert
            Assert.That(vm.CachedSortedColumn, Is.EqualTo("Country"));
        }

        [Test]
        public void Sort_WhenSortingOnCountryDescending_SetsCachedSortedColumn()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("Country", SortDirection.Descending);

            // Assert
            Assert.That(vm.CachedSortedColumn, Is.EqualTo("Country"));
        }

        [Test]
        public void Sort_WhenSortingOnLtv_SetsCachedSortedColumn()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("LTV", SortDirection.Ascending);

            // Assert
            Assert.That(vm.CachedSortedColumn, Is.EqualTo("LTV"));
        }

        [Test]
        public void Sort_WhenSortingOnLtvDescending_SetsCachedSortedColumn()
        {
            // Arrange
            var vm = new MainViewModel(_provider);

            // Act
            vm.SortData("LTV", SortDirection.Descending);

            // Assert
            Assert.That(vm.CachedSortedColumn, Is.EqualTo("LTV"));
        }
    }
}
