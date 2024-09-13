using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IService;
using PathFinder.DataTransferObjects.DTOs.Role;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace STS.API.Controllers.Definition
{
    [Route("Role")]
    [ApiController]
    public class RoleController : CrudWithPaginateController<Role, RoleDTO, RoleDTO, RoleDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IRoleService _service;

        public RoleController(IMapper mapper, IRoleService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
        }
    }
}