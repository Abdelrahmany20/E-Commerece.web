using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configration
{
    class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Brand)
                 .WithMany(B => B.Products)
                 .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.Type)
                 .WithMany(B => B.Products)
                 .HasForeignKey(p => p.TypeId);

            builder.Property(p => p.Price)
                 .HasColumnType("decimal(18,3)"); 


        }
    }
}
