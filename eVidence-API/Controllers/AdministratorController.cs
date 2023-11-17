using Microsoft.AspNetCore.Mvc;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using eVidence_API.Context;
using System.Xml.Linq;
using eVidence_API.Enums;
using eVidence_API.Services;
using System.Reflection;

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
                    if (context.Administrators.Where(a => a.Login == login).Any())
                    {
                        return new Response { Success = false, Result = "Administrator with this login already exists" };
                    }

                    context.Administrators.Add(new Administrator { 
                        Login = login, 
                        Password = HashService.CreateHash(password),
                        PermissionAdministrator = permissionAdministrator,
                        PermissionUser = permissionUser, 
                        PermissionProcess = permissionProcess, 
                        PermissionReport = permissionReport 
                    });
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"AdministratorController, {MethodBase.GetCurrentMethod()}", MethodBase.GetCurrentMethod().GetParameters());
                    return new Response { Success = false };
                }
            }

            return new Response();
        }

        [HttpGet, Route("")]
        public Response GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    return new Response { Result = context.Administrators.ToArray() };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"AdministratorController, {MethodBase.GetCurrentMethod()}", MethodBase.GetCurrentMethod().GetParameters());
                    return new Response { Success = false };
                }
            }
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
                        return new Response { Success = false, Result = "No administrator found for this id" };

                    return new Response { Result = administrator.Single() };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"AdministratorController, {MethodBase.GetCurrentMethod()}", MethodBase.GetCurrentMethod().GetParameters());
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
                    var administrator = context.Administrators.Where(a => a.Login == login);
                    if (!administrator.Any())
                        return new Response { Success = false, Result = "No administrator found for the credentials" };

                    if (!HashService.VerifyHash(password, administrator.Single().Password))
                        return new Response { Success = false, Result = "Password incorrect" };

                    return new Response { Result = administrator.Single() };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"AdministratorController, {MethodBase.GetCurrentMethod()}", MethodBase.GetCurrentMethod().GetParameters());
                    return new Response { Success = false };
                }
            }
        }

        [HttpPost, Route("changepassword")]
        public Response ChangePassword(string login, string oldPassword, string newPassword)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var administrator = context.Administrators.Where(a => a.Login == login);
                    if (!administrator.Any())
                        return new Response { Success = false, Result = "No administrator found for the credentials" };

                    if (!HashService.VerifyHash(oldPassword, administrator.Single().Password))
                        return new Response { Success = false, Result = "Password is incorect" };

                    administrator.Single().Password = HashService.CreateHash(newPassword);
                    context.SaveChanges();

                    return new ();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"AdministratorController, {MethodBase.GetCurrentMethod()}", MethodBase.GetCurrentMethod().GetParameters());
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
                        return new Response { Success = false, Result = "No administrator found for the credentials" };

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
                        return new Response { Success = false, Result = "No administrator found for the credentials" };

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