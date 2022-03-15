using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class CrmService
    {
        private readonly CrmContext _context;

        public CrmService(CrmContext context)
        {
            _context = context;
        }

        //public Blog AddBlog(string name, string url)
        //{
        //    var blog = _context.Blogs.Add(new Blog { Name = name, Url = url });
        //    _context.SaveChanges();

        //    return blog;
        //}

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.Include(c => c.Emails).Include(c => c.Invoices).ThenInclude(i => i.LineItems).OrderBy(c => c.Name).ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.OrderBy(p => p.Sku).ToList();
        }
    }
}