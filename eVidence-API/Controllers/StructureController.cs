using Microsoft.AspNetCore.Mvc;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using eVidence_API.Context;

namespace eVidence_API.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class StructureController : ControllerBase
    {
        private readonly ILogger<StructureController> _logger;

        public StructureController(ILogger<StructureController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("department/{id}")]
        public Response GetDepartment(int id)
        {
            // TODO
            return new Response { Success = false };
        }
    }
}