using AutoMapper;
using STS.Core.Entities;
using STS.DataTransferObjects.DTOs.CartItem;
using STS.DataTransferObjects.DTOs.Product;
using STS.DataTransferObjects.Helpers;

namespace STS.BusinessLogic.Mapping
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<AddUpdateCartItemDTO, CartItem>().ReverseMap();
            CreateMap<QuantityUpdateDTO , CartItem>().ReverseMap();

            CreateMap<CartItem, GetPageCartItemDTO>()
                .ForMember(e => e.ProductName, ex => ex.MapFrom(src => src.Product.Name))
                .ForMember(e => e.ProductId, ex => ex.MapFrom(src => src.Product.Id))
                .ForMember(e => e.Price, ex => ex.MapFrom(src => src.Product.Price))

                .ReverseMap();

            CreateMap<Pagination<CartItem>, Pagination<GetPageCartItemDTO>>().ReverseMap();

        }
    }
}