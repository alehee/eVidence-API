using Microsoft.AspNetCore.Mvc;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using eVidence_API.Context;
using System.Xml.Linq;
using eVidence_API.Enums;

namespace eVidence_API.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class AdministratorController : ControllerBase
    {
        private readonly ILogger<AdministratorController> _logger;

        public AdministratorController(ILogger<AdministratorController> logger)
        {
            _logger = logger;
        }

        [HttpPost, Route("")]
        public Response Create(string login, string password, bool permissionAdministrator, bool permissionUser, bool permissionProcess, bool permissionReport)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    context.Administrators.Add(new Administrator { 
                        Login = login, 
                        Password = password,
                        PermissionAdministrator = permissionAdministrator,
                        PermissionUser = permissionUser, 
                        PermissionProcess = permissionProcess, 
                        PermissionReport = permissionReport 
                    });
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "AdministratorController, Create", null);
                    return new Response { Success = false };
                }
            }

            return new Response();
        }

        [HttpGet, Route("{id}")]
        public Response Get(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var administrator = context.Administrators.Where(a => a.Id == id);
                    if (!administrator.Any())
                        new Response { Success = false, Result = "No administrator found for this id" };

                    return new Response { Result = administrator.Single() };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "AdministratorController, Get", null);
                    return new Response { Success = false };
                }
            }
        }

        [HttpPost, Route("authenticate")]
        public Response Authenticate(string login, string password)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var administrator = context.Administrators.Where(a => a.Login == login && a.Password == password);
                    if (!administrator.Any())
                        new Response { Success = false, Result = "No administrator found for the credentials" };

                    return new Response { Result = administrator.Single() };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "AdministratorController, Authenticate", null);
                    return new Response { Success = false };
                }
            }
        }

        [HttpPut, Route("{id}")]
        public Response Edit(int id, string login, string password, bool permissionAdministrator, bool permissionUser, bool permissionProcess, bool permissionReport)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var administrator = context.Administrators.Where(a => a.Id == id);
                    if (!administrator.Any())
                        new Response { Success = false, Result = "No administrator found for the credentials" };

                    var singleAdministrator = administrator.Single();
                    singleAdministrator.Login = login;
                    singleAdministrator.Password = password;
                    singleAdministrator.PermissionAdministrator = permissionAdministrator;
                    singleAdministrator.PermissionUser = permissionUser;
                    singleAdministrator.PermissionProcess = permissionProcess;
                    singleAdministrator.PermissionReport = permissionReport;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "AdministratorController, Edit", null);
                    return new Response { Success = false };
                }
            }

            return new Response();
        }

        [HttpDelete, Route("{id}")]
        public Response Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var administrator = context.Administrators.Where(a => a.Id == id);
                    if (!administrator.Any())
                        new Response { Success = false, Result = "No administrator found for the credentials" };

                    context.Administrators.Remove(administrator.Single());
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "AdministratorController, Delete", null);
                    return new Response { Success = false };
                }
            }

            return new Response();
        }
    }
}