using Microsoft.AspNetCore.Mvc;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using eVidence_API.Context;

namespace eVidence_API.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("check")]
        public Response Check(string keycard)
        {
            // TODO
            return null;
        }

        [HttpPost, Route("register")]
        public Response Register(string keycard, string name, string surname, int departmentId)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    context.Accounts.Add(new Account { 
                        Keycard = keycard, 
                        Name = name, 
                        Surname = surname, 
                        Department = context.Departments.Where(a => a.Id == departmentId).Single()
                    });
                    context.SaveChanges();
                } catch (Exception ex)
                {
                    _logger.LogError(ex, "AccountController, Register", null);
                    return new Response { Success = false };
                }
            }

            return new Response();
        }

        [HttpPost, Route("login")]
        public Response Login(string keycard)
        {
            // TODO
            return null;
        }
    }
}