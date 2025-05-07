using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Exeptions;
using Domain.Models.Products;
using Services.Specifications;
using Shared;
using Shared.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    class ProductServices(IUnitOfWork unitOfWork,IMapper mapper) : IProductServices
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var _Repository = unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await _Repository.GetAllAsync();

            var MappedBrands = mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return MappedBrands;
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductAsync(ProductQueryParams productQueryParams)
        {
            var _Repository = unitOfWork.GetRepository<Product, int>();

            var spec = new ProductWithBrand_TypeSpecification(productQueryParams );

            var Products = await _Repository.GetAllAsync(spec);

            var MappedProducts = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products );

            var CountedProducts= Products.Count();


            var CountSpec = new ProductCountSpecification(productQueryParams );
            var TotalCount = await _Repository.CountAsync(CountSpec);


            return new  PaginatedResult<ProductDto>(productQueryParams.PageIndex, CountedProducts,TotalCount ,MappedProducts);

        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var _Repository = unitOfWork.GetRepository<ProductType, int>();
            var Types = await _Repository.GetAllAsync();

            var MappedTypes = mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return MappedTypes;


        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {

             var spec = new ProductWithBrand_TypeSpecification(id);




            var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);


            if (Product == null)
                throw new ProductNotFoundExecption(id);



            return mapper.Map<Product, ProductDto>(Product);
        }
    }
}
