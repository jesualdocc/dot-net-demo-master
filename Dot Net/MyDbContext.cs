
using APP_Demo__WebAPI_.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_API
{
    public class MyDbContext:DbContext
    {
        public DbSet<Metadata> Metadatas { get; set; }
        public DbSet<DrugInfo> DrugInfos { get; set; }


        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
        
    }
}
