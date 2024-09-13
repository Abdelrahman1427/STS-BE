using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Core.Interface.IService;

namespace Dashboard.API.Controllers.DataEntry
{
    [Route("TOT")]
    [ApiController]
    public class TOTController : ControllerBase
    {
        private readonly ITOTService _tOTService;
        public TOTController(ITOTService tOTService)
        {
            _tOTService = tOTService;
        }
    }
}
