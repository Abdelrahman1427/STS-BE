using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.Assessment;
using PathFinder.DataTransferObjects.DTOs.Certification;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace Dashboard.API.Controllers.Definition
{
    [Route("Assessment")]
    [ApiController]
    public class AssessmentController : CrudWithPaginateController<Assessment, AssessmentDTO, AssessmentDTO, CertificationDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IAssessmentService _service;

        public AssessmentController(IMapper mapper, IAssessmentService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}