
//using AutoMapper;
//using STS.API.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using PathFinder.Core.Entities;
//using PathFinder.Core.Interface.IService;
//using PathFinder.DataTransferObjects.DTOs.Shared.Request;
//using PathFinder.DataTransferObjects.DTOs.User;
//using System.Linq.Expressions;

//namespace STS.API.Controllers.Definition
//{
//    [Route("User")]
//    [ApiController]
//    public class UserController : CrudWithPaginateController<User, UserDTO, UserDTO, UserDTO, GetLookUpDefinitionDTO>
//    {
//        private IMapper _mapper;
//        private IUserService _service;
//        Expression<Func<User, bool>> _predicate;

//        public UserController(IMapper mapper, IUserService service) : base(mapper, service)
//        {
//            _mapper = mapper;
//            _service = service;
//        }
//    }
//}
