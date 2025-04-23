using AutoMapper;
using Domain.Models.Products;
using Shared.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {


        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dist => dist.TypeName, opt => opt.MapFrom(src => src.Type.Name))
                .ForMember(dist => dist.PictureUrl, opt => opt.MapFrom<ProductReslover>());
                //.ReverseMap();

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();


        }


    }
}
