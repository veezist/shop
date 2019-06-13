using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class OurDbContext:DbContext
    {
        public OurDbContext() : base() { }

        public DbSet<User> User { get; set; }

        public DbSet<Item> Items { get; set; }

        
    }
}