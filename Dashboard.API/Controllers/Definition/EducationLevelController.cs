using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.EducationLevel;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace Dashboard.API.Controllers.Definition
{
  
    [Route("EducationLevel")]
    [ApiController]
    public class EducationLevelController : CrudWithPaginateController<EducationLevel, EducationLevelDTO, EducationLevelDTO, EducationLevelDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IEducationLevelService _service;

        public EducationLevelController(IMapper mapper, IEducationLevelService service, IHttpContextAccessor context) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}