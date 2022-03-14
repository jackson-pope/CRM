﻿using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class CrmService
    {
        private CrmContext _context;

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
            return _context.Customers.Include(c => c.Invoices).Include(c => c.Emails).OrderBy(c => c.Name).ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.OrderBy(p => p.Sku).ToList();
        }
    }
}