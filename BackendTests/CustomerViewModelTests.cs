using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Backend.Models;
using Backend.ViewModels;

namespace BackendTests
{
    [TestFixture]
    internal class CustomerViewModelTests
    {
        [Test]
        public void Constructor_SetsName_Correctly()
        {
            // Arrange
            var customer = new Customer { Name = "Bob" };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>());

            // Assert
            Assert.That(vm.Name, Is.EqualTo(customer.Name));
        }

        [Test]
        public void Constructor_SetsId_Correctly()
        {
            // Arrange
            var customer = new Customer { Id = 11 };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>());

            // Assert
            Assert.That(vm.Id, Is.EqualTo(customer.Id));
        }

        [Test]
        public void Constructor_SetsPhoneNumber_Correctly()
        {
            // Arrange
            var customer = new Customer { PhoneNumber = "999" };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>());

            // Assert
            Assert.That(vm.PhoneNumber, Is.EqualTo(customer.PhoneNumber));
        }

        [Test]
        public void Constructor_SetsCountryCode_Correctly()
        {
            // Arrange
            var customer = new Customer { Country = "UK" };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>());

            // Assert
            Assert.That(vm.CountryCode, Is.EqualTo(customer.Country));
        }

        [Test]
        public void Constructor_SetsAcquisition_Correctly()
        {
            // Arrange
            var customer = new Customer { Acquisition = "Pub" };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>());

            // Assert
            Assert.That(vm.Acquisition, Is.EqualTo(customer.Acquisition));
        }

        [Test]
        public void Constructor_WithOneEmail_SetsEmailCorrectly()
        {
            // Arrange
            var customer = new Customer { Emails = new List<Email> { new Email { EmailAddress = "bob@example.com" } } };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>());

            // Assert
            Assert.That(vm.EmailAddresses, Is.EqualTo(customer.Emails.First().EmailAddress));
        }

        [Test]
        public void Constructor_WithTwoEmails_SetsEmailCorrectly()
        {
            // Arrange
            var customer = new Customer { Emails = new List<Email> { new Email { EmailAddress = "bob@example.com" }, new Email { EmailAddress = "bob@work.com" } } };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>());

            // Assert
            Assert.That(vm.EmailAddresses, Is.EqualTo("bob@example.com\nbob@work.com"));
        }
        [Test]
        public void Constructor_WithFullAddress_SetsAddressCorrectly()
        {
            // Arrange
            var customer = new Customer { Address1 = "Line1", Address2 = "Line2", Address3 = "Line3", Address4 = "Line4", Address5 = "Line5" };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>());

            // Assert
            Assert.That(vm.Address, Is.EqualTo("Line1\nLine2\nLine3\nLine4\nLine5"));
        }

        [Test]
        public void Constructor_WithEmptyAddressLine_SetsAddressCorrectly()
        {
            // Arrange
            var customer = new Customer { Address1 = "Line1", Address2 = "Line2", Address3 = "", Address4 = "Line4", Address5 = "Line5" };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>());

            // Assert
            Assert.That(vm.Address, Is.EqualTo("Line1\nLine2\nLine4\nLine5"));
        }

        [Test]
        public void Constructor_WithNullAddressLine_SetsAddressCorrectly()
        {
            // Arrange
            var customer = new Customer { Address1 = "Line1", Address2 = "Line2", Address3 = null, Address4 = "Line4", Address5 = "Line5" };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>());

            // Assert
            Assert.That(vm.Address, Is.EqualTo("Line1\nLine2\nLine4\nLine5"));
        }

        [Test]
        public void Constructor_WithNoOrders_SetsProductsCorrectly()
        {
            // Arrange
            var customer = new Customer { };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>());

            // Assert
            Assert.True(string.IsNullOrEmpty(vm.Products));
        }

        [Test]
        public void Constructor_WithSingleProduct_SetsProductsCorrectly()
        {
            // Arrange
            var customer = new Customer
            {
                Invoices = new List<Invoice> { new Invoice { LineItems = new List<LineItem>
                                                                            {
                                                                                new LineItem { Product = "EYD0003", Quantity = 1 }
                                                                            } } }
            };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>
                                                        {
                                                            new Product { Sku = "EYD0003", Description = "Deluxe FlickFleet" },
                                                            new Product { Sku = "EYD0004", Description = "Standard FlickFleet"  }
                                                        });

            // Assert
            Assert.That(vm.Products, Is.EqualTo("1x Deluxe FlickFleet"));
        }

        [Test]
        public void Constructor_WithOneOrderOfTwoIdenticalProducts_ListsProductOnceWithCorrectQuantity()
        {
            // Arrange
            var customer = new Customer
            {
                Invoices = new List<Invoice> { new Invoice { LineItems = new List<LineItem>
                                                                            {
                                                                                new LineItem { Product = "EYD0003", Quantity = 2 }
                                                                            } } }
            };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>
                                                        {
                                                            new Product { Sku = "EYD0003", Description = "Deluxe FlickFleet" },
                                                            new Product { Sku = "EYD0004", Description = "Standard FlickFleet"  }
                                                        });

            // Assert
            Assert.That(vm.Products, Is.EqualTo("2x Deluxe FlickFleet"));
        }

        [Test]
        public void Constructor_WithTwoOrdersOfSameProduct_ListsProductOnceWithCorrectQuantity()
        {
            // Arrange
            var customer = new Customer
            {
                Invoices = new List<Invoice> { new Invoice { LineItems = new List<LineItem>
                                                                            {
                                                                                new LineItem { Product = "EYD0003", Quantity = 1 },
                                                                                new LineItem { Product = "EYD0003", Quantity = 1 }
                                                                            } } }
            };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>
                                                        {
                                                            new Product { Sku = "EYD0003", Description = "Deluxe FlickFleet" },
                                                            new Product { Sku = "EYD0004", Description = "Standard FlickFleet"  }
                                                        });

            // Assert
            Assert.That(vm.Products, Is.EqualTo("2x Deluxe FlickFleet"));
        }

        [Test]
        public void Constructor_WithTwoProducts_ListsBoth()
        {
            // Arrange
            var customer = new Customer
            {
                Invoices = new List<Invoice> { new Invoice { LineItems = new List<LineItem>
                                                                            {
                                                                                new LineItem { Product = "EYD0003", Quantity = 1 },
                                                                                new LineItem { Product = "EYD0004", Quantity = 2 }
                                                                            } } }
            };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product>
                                                        {
                                                            new Product { Sku = "EYD0003", Description = "Deluxe FlickFleet" },
                                                            new Product { Sku = "EYD0004", Description = "Standard FlickFleet"  }
                                                        });

            // Assert
            Assert.That(vm.Products, Is.EqualTo("1x Deluxe FlickFleet\n2x Standard FlickFleet"));
        }

        [Test]
        public void Constructor_WithTwoProducts_ListsInSkuOrder()
        {
            // Arrange
            var customer = new Customer
            {
                Invoices = new List<Invoice> { new Invoice { LineItems = new List<LineItem>
                                                                            {
                                                                                new LineItem { Product = "EYD0004", Quantity = 1 },
                                                                                new LineItem { Product = "EYD0003", Quantity = 2 }
                                                                            } } }
            };

            // Act
            var vm = new CustomerViewModel(customer, new List<Product> 
                                                        { 
                                                            new Product { Sku = "EYD0003", Description = "Deluxe FlickFleet" }, 
                                                            new Product { Sku = "EYD0004", Description = "Standard FlickFleet"  } 
                                                        });

            // Assert
            Assert.That(vm.Products, Is.EqualTo("2x Deluxe FlickFleet\n1x Standard FlickFleet"));
        }
    }
}
