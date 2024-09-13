using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IService;
using PathFinder.DataTransferObjects.DTOs.CourseTransaction;


namespace Dashboard.API.Controllers.DataEntry
{
    [Route("Beneficiaries")]
    [ApiController]
    public class BeneficiariesController : CrudWithPaginateController<Beneficiarie, BeneficiarieDTO, BeneficiarieDTO, BeneficiarieDTO, GetPageBeneficiarieDTO>
    {
        private readonly IBeneficiariesService _beneficiariesService;
        private readonly IHttpContextAccessor _httpContext;
        private IMapper _mapper;


        public BeneficiariesController(IMapper mapper, IBeneficiariesService beneficiariesService, IHttpContextAccessor httpContext) : base(mapper, beneficiariesService)
        {
            _mapper = mapper;
            _beneficiariesService = beneficiariesService;
            _httpContext = httpContext;
        }

    }
}
