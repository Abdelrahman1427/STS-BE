using AutoMapper;
using STS.Core.Entities;
using STS.DataTransferObjects.DTOs.CartItem;
using STS.DataTransferObjects.Helpers;

namespace STS.BusinessLogic.Mapping
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<AddUpdateCartItemDTO, CartItem>().ReverseMap();

            CreateMap<CartItem, GetPageCartItemDTO>()
                .ForMember(e =>e.ProductName , ex => ex.MapFrom(src =>src.Product.Name))
                .ReverseMap();

            CreateMap<Pagination<CartItem>, Pagination<GetPageCartItemDTO>>().ReverseMap();
        }
    }
}