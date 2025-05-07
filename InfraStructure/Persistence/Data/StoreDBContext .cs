using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
   public class StoreDBContext(DbContextOptions<StoreDBContext> options) : DbContext(options)
    {

        


        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductBrand> ProductBrands { get; set; } = null!;

        public DbSet<ProductType> ProductTypes { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
       =>modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);


    }
}
