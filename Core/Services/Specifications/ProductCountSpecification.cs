using Domain.Models.Products;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductCountSpecification : BaseSpecifications<Product,int>
    {
        public ProductCountSpecification(ProductQueryParams productQueryParams)
    : base(p => (!productQueryParams.BrandId.HasValue || p.BrandId == productQueryParams.BrandId)
     && (!productQueryParams.TypeId.HasValue || p.TypeId == productQueryParams.TypeId)
    && (string.IsNullOrEmpty(productQueryParams.SearchValue) || p.Name.ToLower().Contains(productQueryParams.SearchValue.ToLower())))
        {



        }


    }
}
