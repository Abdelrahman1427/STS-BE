using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.CourseTransaction;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class BeneficiarieProfile : Profile
    {
        public BeneficiarieProfile()
        {
            CreateMap<BeneficiarieDTO, Beneficiarie>().ReverseMap();
            CreateMap<Beneficiarie, GetPageBeneficiarieDTO>().ReverseMap();
            CreateMap<Pagination<Beneficiarie>, Pagination<GetPageBeneficiarieDTO>>().ReverseMap();
        }
    }
}