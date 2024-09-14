using AutoMapper;
using STS.Core.Entities;
using STS.DataTransferObjects.DTOs.Product;
using STS.DataTransferObjects.Helpers;

namespace STS.BusinessLogic.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddUpdateProductDTO, Product>().ReverseMap();

            CreateMap<Product, GetPageProductDTO>()
                .ForMember(e => e.CategoryName , ex => ex.MapFrom(src =>src.Category.Name))
                .ReverseMap();

            CreateMap<Pagination<Product>, Pagination<GetPageProductDTO>>().ReverseMap();
        }
    }
}