using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers.Identity
{
    [ApiController]
    [Route("Role")]

    public class RoleController //: CrudWithPaginateController<Role, RoleDTO, RoleDTO, RoleDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private readonly IHttpContextAccessor _context;
        public RoleController(IHttpContextAccessor context, IMapper mapper)//, IRoleService service) : base(mapper, service)
        {
            _mapper = mapper;
            _context = context;
        }
    }
}
