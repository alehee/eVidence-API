﻿using eVidence_API.Context;
using eVidence_API.Enums;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace eVidence_API.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class CheckpointController : ControllerBase
    {
        private readonly ILogger<CheckpointController> _logger;

        public CheckpointController(ILogger<CheckpointController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("check")]
        public Response Check(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    return new Response { Success = false };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CheckpointController, Check", null);
                return new Response { Success = false };
            }
        }
    }
}
