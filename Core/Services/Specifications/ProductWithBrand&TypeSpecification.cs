using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
   public class ProductWithBrand_TypeSpecification :BaseSpecifications<Product,int>
    {
        public ProductWithBrand_TypeSpecification() :base(null)
        {
             AddInclude(p=>p.Brand);
            AddInclude(p => p.Type);
        }




    }
}
