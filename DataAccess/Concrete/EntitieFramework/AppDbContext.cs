
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntitieFramework
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Zay_ShopDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public DbSet<Banner> Banners { get; set; }   
        public DbSet<Category> Categories { get; set; }
    }
}
