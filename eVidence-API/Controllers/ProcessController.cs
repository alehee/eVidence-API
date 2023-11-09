using eVidence_API.Context;
using eVidence_API.Enums;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public Response PostAdd(int groupId, string name, string shortName, string colorCode)
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
                _logger.LogError(ex, "ProcessController, PostAdd", null);
                return new Response { Success = false };
            }
        }

        [HttpGet, Route("")]
        public Response GetAll()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    return new Response { Result = context.Processes.Include("Group").ToArray() };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProcessController, GetAll", null);
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

        [HttpPut, Route("{id}")]
        public Response PutUpdate(int id, int groupId, string name, string shortName, string colorCode)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var process = context.Processes.Where(a => a.Id == id);
                    if (!process.Any())
                        return new Response { Success = false, Result = "Process with this id not exists" };

                    var group = context.Groups.Where(a => a.Id == groupId);
                    if (!group.Any())
                        return new Response { Success = false, Result = "Group with this id not exists" };

                    var singleProcess = process.Single();
                    var singleGroup = group.Single();

                    singleProcess.Group = singleGroup;
                    singleProcess.Name = name;
                    singleProcess.ShortName = shortName;
                    singleProcess.Color = colorCode;
                    context.SaveChanges();

                    return new();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProcessController, PutUpdate", null);
                return new Response { Success = false };
            }
        }

        [HttpDelete, Route("{id}")]
        public Response Delete(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var process = context.Processes.Where(a => a.Id == id);
                    if (!process.Any())
                        return new Response { Success = false, Result = "Process with this id not exists" };

                    context.Processes.Remove(process.Single());
                    context.SaveChanges();

                    return new();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProcessController, Delete", null);
                return new Response { Success = false };
            }
        }
    }
}
