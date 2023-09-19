using eVidence_API.Context;
using eVidence_API.Enums;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace eVidence_API.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly ILogger<ProcessController> _logger;

        public ProcessController(ILogger<ProcessController> logger)
        {
            _logger = logger;
        }

        [HttpPost, Route("")]
        public Response Add(int groupId, string name, string shortName, string colorCode)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var group = context.Groups.Where(a => a.Id == groupId);

                    if(!group.Any())
                    {
                        return new Response { Success = false, Result = "Group with this id not exists" };
                    }

                    System.Console.WriteLine(colorCode.Remove(0, 1));
                    context.Processes.Add(new Process { Group = group.Single(), Name = name, ShortName = shortName, Color = colorCode });
                    context.SaveChanges();

                    return new Response();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProcessController, Check", null);
                return new Response { Success = false };
            }
        }

        [HttpGet, Route("{id}")]
        public Response Get(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var process = context.Processes.Where(a => a.Id == id);

                    if (!process.Any())
                    {
                        return new Response { Success = false, Result = "Process with this id not exists" };
                    }

                    return new Response { Result = process.Single() };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProcessController, Get", null);
                return new Response { Success = false };
            }
        }
    }
}
