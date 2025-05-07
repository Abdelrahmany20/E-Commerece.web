 using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation.Controllers
{
    public class ProductController(IServicesManger servicesManger) : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams productQueryParams)
        {
            var Products = await servicesManger.ProductServices.GetAllProductAsync(productQueryParams);
            return Ok(Products);
        }




        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await servicesManger.ProductServices.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("Tybes")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTybes()
        {
            var Tybes = await servicesManger.ProductServices.GetAllTypesAsync();
            return Ok(Tybes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await servicesManger.ProductServices.GetProductByIdAsync(id);
            return Ok(product);
        }

    }
}
