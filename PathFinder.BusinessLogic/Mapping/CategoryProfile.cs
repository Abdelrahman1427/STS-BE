using AutoMapper;
using STS.Core.Entities;
using STS.DataTransferObjects.DTOs.Category;
using STS.DataTransferObjects.Helpers;

namespace STS.BusinessLogic.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<AddUpdateCategoryDTO, Category>().ReverseMap();

            CreateMap<Category, GetPageCategoryDTO>().ReverseMap();

            CreateMap<Pagination<Category>, Pagination<GetPageCategoryDTO>>().ReverseMap();
        }
    }
}