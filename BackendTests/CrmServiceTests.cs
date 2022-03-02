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

            var customers = service.GetAllCustomers();

            Assert.AreEqual(3, customers.Count);
            Assert.AreEqual("Archie",  customers[0].Name);
            Assert.AreEqual("Bob",     customers[1].Name);
            Assert.AreEqual("Charlie", customers[2].Name);
        }
    }
}