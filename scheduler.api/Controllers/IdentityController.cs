using Microsoft.AspNetCore.Mvc;
using scheduler.core.Auth;
using scheduler.core.Dtos.Requests;
using scheduler.core.Dtos.Responses;
using scheduler.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scheduler.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var result = _identityService.GetAll();

            return Ok(result);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            var authResponse = await _identityService.RegisterAsync(dto);

            return authResponse.Success
                ? Ok(new AuthSuccessResponse { Token = authResponse.Token })
                : BadRequest(new AuthFailureResponse { ErrorMessages = authResponse.ErrorMessages });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            var authResponse = await _identityService.LoginAsync(request);

            return authResponse.Success
                ? Ok(new AuthSuccessResponse { Token = authResponse.Token })
                : BadRequest(new AuthFailureResponse { ErrorMessages = authResponse.ErrorMessages });
        }
    }
}
