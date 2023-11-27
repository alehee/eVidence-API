using Microsoft.AspNetCore.Mvc;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using eVidence_API.Context;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet, Route("group")]
        public Response GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    return new Response { Result = context.Groups.ToArray() };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"StructureController, {MethodBase.GetCurrentMethod().Name}", MethodBase.GetCurrentMethod().GetParameters());
                    return new Response { Success = false };
                }
            }
        }

        [HttpPost, Route("group/add")]
        public Response AddGroup(string name)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    if (context.Groups.Where(a => a.Name == name).Any())
                        return new Response { Success = false, Result = "Group with that name already exists" };

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
                        return new Response { Success = false, Result = "Group with this id not exists" };

                    return new Response { Result = groups.Single() };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"StructureController, {MethodBase.GetCurrentMethod().Name}", MethodBase.GetCurrentMethod().GetParameters());
                    return new Response { Success = false };
                }
            }
        }

        [HttpGet, Route("group/{id}/departments")]
        public Response GetGroupDepartments(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var groups = context.Groups.Where(a => a.Id == id).Include("Departments");

                    if (!groups.Any())
                        return new Response { Success = false, Result = "Group with this id not exists" };

                    return new Response { Result = groups.Single().Departments.ToArray() };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"StructureController, {MethodBase.GetCurrentMethod().Name}", MethodBase.GetCurrentMethod().GetParameters());
                    return new Response { Success = false };
                }
            }
        }

        [HttpPut, Route("group/{id}")]
        public Response EditGroup(int id, string name)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var groups = context.Groups.Where(a => a.Id == id);

                    if (!groups.Any())
                        return new Response { Success = false, Result = "Group with this id not exists" };

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
                        return new Response { Success = false, Result = "Group with this id not exists" };

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

        #region GroupDepartment
        [HttpPost, Route("group/{id}/{departmentId}")]
        public Response AddDepartmentToGroup(int id, int departmentId)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var groups = context.Groups.Where(a => a.Id == id).Include("Departments");
                    var departments = context.Departments.Where(a => a.Id == departmentId);

                    if (!groups.Any())
                        return new Response { Success = false, Result = "Group with this id not exists" };

                    if (!departments.Any())
                        return new Response { Success = false, Result = "Department with this id not exists" };

                    groups.Single().Departments.Add(departments.Single());
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

        [HttpDelete, Route("group/{id}/{departmentId}")]
        public Response DeleteDepartmentFromGroup(int id, int departmentId)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var groups = context.Groups.Where(a => a.Id == id).Include("Departments");
                    var departments = context.Departments.Where(a => a.Id == departmentId);

                    if (!groups.Any())
                        return new Response { Success = false, Result = "Group with this id not exists" };

                    if (!departments.Any())
                        return new Response { Success = false, Result = "Department with this id not exists" };

                    groups.Single().Departments.Remove(departments.Single());
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
        [HttpGet, Route("department")]
        public Response GetDepartments()
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    return new Response { Result = context.Departments.Include("Groups").ToArray() };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"StructureController, {MethodBase.GetCurrentMethod().Name}", MethodBase.GetCurrentMethod().GetParameters());
                    return new Response { Success = false };
                }
            }
        }

        [HttpPost, Route("department/add")]
        public Response AddDepartment(string name)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    if (context.Departments.Where(a => a.Name == name).Any())
                        return new Response { Success = false, Result = "Department with that name already exists" };

                    context.Departments.Add(new Department { Name = name});
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

        [HttpGet, Route("department/{id}")]
        public Response GetDepartment(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var departments = context.Departments.Where(a => a.Id == id);

                    if (!departments.Any())
                        return new Response { Success = false, Result = "Department with this id not exists" };

                    return new Response { Result = departments.Single() };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"StructureController, {MethodBase.GetCurrentMethod().Name}", MethodBase.GetCurrentMethod().GetParameters());
                    return new Response { Success = false };
                }
            }
        }

        [HttpGet, Route("department/{id}/groups")]
        public Response GetDepartmentGroups(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var departments = context.Departments.Where(a => a.Id == id).Include("Groups");

                    if (!departments.Any())
                        return new Response { Success = false, Result = "Department with this id not exists" };

                    return new Response { Result = departments.Single().Groups.ToArray() };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"StructureController, {MethodBase.GetCurrentMethod().Name}", MethodBase.GetCurrentMethod().GetParameters());
                    return new Response { Success = false };
                }
            }
        }

        [HttpPut, Route("department/{id}")]
        public Response EditDepartment(int id, string name)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var departments = context.Departments.Where(a => a.Id == id);

                    if (!departments.Any())
                        return new Response { Success = false, Result = "Department with this id not exists" };

                    var department = departments.Single();
                    department.Name = name;
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

        [HttpDelete, Route("department/{id}")]
        public Response DeleteDepartment(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var departments = context.Departments.Where(a => a.Id == id);

                    if (!departments.Any())
                        return new Response { Success = false, Result = "Department with this id not exists" };

                    context.Departments.Remove(departments.Single());
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
    }
}