using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Services_Abstraction.IServices;
using ECommerce.Shared.Dtos.IdentityDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var Result = await _authenticationService.LoginAsync(loginDto);
            return HandleResult(Result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var Result = await _authenticationService.RegisterAsync(registerDto);
            return HandleResult(Result);
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result = await _authenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }

        [Authorize]
        [HttpGet("GetUserByEmail")]
        public async Task<ActionResult<UserDto>> GetUserByEmail()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email)!;
            var result = await _authenticationService.GetUserByEmailAsync(Email);
            return HandleResult(result);
        }
    }
}
