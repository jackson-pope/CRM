using System.Collections.Generic;

using NUnit.Framework;

using Backend.Models;
using Backend;

namespace BackendTests
{
    [TestFixture]
    public class CustomerOverviewTests
    {
        [Test]
        public void Constructor_PopulatesWithFullCustomerHighlights()
        {
            // Arrange
            var customer = new Customer { Name = "Archie", Country = "GB", EmailAddress = "archie@example.com" };

            //Act
            var overview = new CustomerOverview(customer);

            //Assert
            Assert.That(overview.Name,         Is.EqualTo(customer.Name));
            Assert.That(overview.Country,      Is.EqualTo(customer.Country));
            Assert.That(overview.EmailAddress, Is.EqualTo(customer.EmailAddress));
        }

        [Test]
        public void LTV_IsEqualToSumOfInvoices()
        {
            // Arrange
            var invoice1 = new Invoice { InvoiceTotal = 120.00M };
            var invoice2 = new Invoice { InvoiceTotal = 54.50M };
            var invoices = new List<Invoice> { invoice1, invoice2 };
            var customer = new Customer { Name = "Archie", Country = "GB", EmailAddress = "archie@example.com", Invoices = invoices };

            //Act
            var overview = new CustomerOverview(customer);

            //Assert
            Assert.That(overview.LTV, Is.EqualTo(invoice1.InvoiceTotal + invoice2.InvoiceTotal));
        }
    }
}
