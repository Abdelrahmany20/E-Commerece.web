using Domain.Models.Products;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
   public class ProductWithBrand_TypeSpecification :BaseSpecifications<Product,int>
    {
        public ProductWithBrand_TypeSpecification(ProductQueryParams productQueryParams ) 
            :base(p=>(!productQueryParams.BrandId.HasValue || p.BrandId==productQueryParams.BrandId)
             && (!productQueryParams.TypeId.HasValue || p.TypeId == productQueryParams.TypeId)
            &&(string.IsNullOrEmpty(productQueryParams.SearchValue) || p.Name.ToLower().Contains(productQueryParams.SearchValue.ToLower() )))


        {
             AddInclude(p=>p.Brand);
            AddInclude(p => p.Type);



            switch
                (productQueryParams.SortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;

                case ProductSortingOptions.priceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.priceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;

                default:
                    break;
            }



            ApplyPaginaition(productQueryParams.PageSize, productQueryParams.PageIndex);
        }

        public ProductWithBrand_TypeSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
      


    }
}
