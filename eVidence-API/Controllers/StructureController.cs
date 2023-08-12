using Microsoft.AspNetCore.Mvc;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using eVidence_API.Context;
using System.Reflection;
using System.Xml.Linq;

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

        #region Group
        [HttpPost, Route("group/add")]
        public Response AddGroup(string name)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    if (context.Groups.Where(a => a.Name == name).Any())
                        return new Response { Result = "Group with that name already exists" };

                    context.Groups.Add(new Group { Name = name });
                    context.SaveChanges();
                    return new Response();
                } catch (Exception ex)
                {
                    _logger.LogError(ex, $"StructureController, {MethodBase.GetCurrentMethod().Name}", MethodBase.GetCurrentMethod().GetParameters());
                    return new Response { Success = false };
                }
            }
        }

        [HttpGet, Route("group/{id}")]
        public Response GetGroup(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var groups = context.Groups.Where(a => a.Id == id);

                    if (!groups.Any())
                        return new Response { Result = "Group with this id not exists" };

                    return new Response { Result = groups.Single() };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"StructureController, {MethodBase.GetCurrentMethod().Name}", MethodBase.GetCurrentMethod().GetParameters());
                    return new Response { Success = false };
                }
            }
        }

        [HttpPost, Route("group/{id}/edit")]
        public Response EditGroup(int id, string name)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var groups = context.Groups.Where(a => a.Id == id);

                    if (!groups.Any())
                        return new Response { Result = "Group with this id not exists" };

                    context.Groups.Where(a => a.Id == id).Single().Name = name;
                    context.SaveChanges();

                    return new Response();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"StructureController, {MethodBase.GetCurrentMethod().Name}", MethodBase.GetCurrentMethod().GetParameters());
                    return new Response { Success = false };
                }
            }
        }

        [HttpDelete, Route("group/{id}")]
        public Response DeleteGroup(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var groups = context.Groups.Where(a => a.Id == id);

                    if (!groups.Any())
                        return new Response { Result = "Group with this id not exists" };

                    context.Groups.Remove(groups.Single());
                    context.SaveChanges();

                    return new Response();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"StructureController, {MethodBase.GetCurrentMethod().Name}", MethodBase.GetCurrentMethod().GetParameters());
                    return new Response { Success = false };
                }
            }
        }
        #endregion

        #region Department
        [HttpPost, Route("department/add")]
        public Response AddDepartment(string name, int groupId)
        {
            // TODO
            return new Response { Success = false };
        }

        [HttpGet, Route("department/{id}")]
        public Response GetDepartment(int id)
        {
            // TODO
            return new Response { Success = false };
        }

        [HttpPost, Route("department/{id}/edit")]
        public Response EditDepartment(int id, string name, int groupId)
        {
            // TODO
            return new Response { Success = false };
        }

        [HttpDelete, Route("department/{id}")]
        public Response DeleteDepartment(int id)
        {
            // TODO
            return new Response { Success = false };
        }
        #endregion
    }
}