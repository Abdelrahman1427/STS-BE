using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.BeneficiariesCourse;
using PathFinder.DataTransferObjects.DTOs.CourseTransaction;
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.BusinessLogic.Mapping
{
    public class CourseTransactionProfile : Profile
    {
        public CourseTransactionProfile()
        {
            CreateMap<CourseTransaction, CourseTransactionDTO>().ReverseMap();

            #region BeneficiariesCourse
            CreateMap<BeneficiariesCourseDTO, CourseTransaction>().ReverseMap();
            CreateMap<CourseTransaction, GetPageBeneficiariesCourseDTO>().ReverseMap();
            CreateMap<Pagination<CourseTransaction>, Pagination<GetPageBeneficiariesCourseDTO>>().ReverseMap();
            #endregion
        }
    }
}