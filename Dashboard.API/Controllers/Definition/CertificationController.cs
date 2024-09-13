using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.Certification;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace Dashboard.API.Controllers.Definition
{
    [Route("Certification")]
    [ApiController]
    public class CertificationController : CrudWithPaginateController<Certification, CertificationDTO, CertificationDTO, CertificationDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private ICertificationService _service;

        public CertificationController(IMapper mapper, ICertificationService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}