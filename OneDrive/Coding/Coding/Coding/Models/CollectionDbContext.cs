using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Coding.Models
{
    public class CollectionDbContext : DbContext
    {
        public CollectionDbContext() : base("collection")
        {

        }
        public DbSet<Collection> collections { get; set; }
        public DbSet<Gallery> galleries { get; set; }
    }
}