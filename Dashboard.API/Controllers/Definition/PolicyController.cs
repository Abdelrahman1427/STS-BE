using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IService.IUserServices;
using PathFinder.DataTransferObjects.DTOs.Policy;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
namespace Dashboard.API.Controllers.Definition
{
    [Route("Policy")]
    [ApiController]
    public class PolicyController : CrudWithPaginateController<Policy, PolicyDTO, PolicyDTO, PolicyDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IPolicyService _service;

        public PolicyController(IMapper mapper, IPolicyService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}