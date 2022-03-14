using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

using Backend.Models;
using Backend;

namespace BackendTests
{
    [TestFixture]
    public class CrmServiceTests
    {
        [Test]
        public void GetAllCustomers_sortsByName()
        {
            // Arrange
            var data = new List<Customer>
                        {
                            new Customer { Name = "Bob" },
                            new Customer { Name = "Archie" },
                            new Customer { Name = "Charlie" },
                        }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CrmContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);

            var service = new CrmService(mockContext.Object);

            // Act
            var customers = service.GetAllCustomers();

            // Assert
            CollectionAssert.AreEqual(new string[] { "Archie", "Bob", "Charlie" }, customers.Select(p => p.Name));
        }

        [Test]
        public void GetAllProducts_ReturnsListSortedBySku()
        {
            // Arrange
            var data = new List<Product>
                        {
                            new Product { Sku = "EYD0010", Description = "Last Game" },
                            new Product { Sku = "EYD0002", Description = "Another Game" },
                            new Product { Sku = "EYD0001", Description = "First Game" },
                        }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CrmContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            var service = new CrmService(mockContext.Object);

            // Act
            var products = service.GetAllProducts();

            // Assert
            CollectionAssert.AreEqual(new string[] { "First Game", "Another Game", "Last Game" }, products.Select(p => p.Description));
        }
    }
}