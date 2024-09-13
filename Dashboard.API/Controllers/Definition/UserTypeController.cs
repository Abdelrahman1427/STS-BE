using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IUserServices;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.DTOs.UserType;

namespace STS.API.Controllers.Definition
{
    [Route("UserType")]
    [ApiController]
    public class UserTypeController : CrudWithPaginateController<UserType, UserTypeDTO, UserTypeDTO, UserTypeDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IUserTypeService _service;

        public UserTypeController(IMapper mapper, IUserTypeService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
        }
    }
}