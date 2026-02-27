using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Shared.Dtos.IdentityDtos;
using ECommerce.Shared.ResultPattern;

namespace ECommerce.Services_Abstraction.IServices
{
    #region AuthenticationService
    public interface IAuthenticationService
    {
        public Task<Result<UserDto>> LoginAsync(LoginDto loginDto);
        public Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto);
        public Task<bool> CheckEmailAsync(string email);
        public Task<Result<UserDto>> GetUserByEmailAsync(string email);
    }
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var User = await _userManager.FindByEmailAsync(email);
            return User is not null;
        }

        public async Task<Result<UserDto>> GetUserByEmailAsync(string email)
        {
            var User = await _userManager.FindByEmailAsync(email);
            if (User is null)
                return Error.NotFound("User.NotFound", $"User With Email: {email} Not Found");
            return new UserDto(User.Email!, User.DisplayName, await GenerateToken(User));
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User is null)
                return Error.InvalidCredentials("User.InvalidCredentials");
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (!IsPasswordValid)
                return Error.InvalidCredentials("User.InvalidCredentials");
            var Token = await GenerateToken(User);
            return new UserDto
            (
                User.Email!,
                User.DisplayName,
                Token
            );
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto)
        {
            var User = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber
            };
            var IdentityResult = await _userManager.CreateAsync(User, registerDto.Password);
            if (IdentityResult.Succeeded)
            {
                var Token = await GenerateToken(User);
                return new UserDto(User.Email!, User.DisplayName, Token);
            }
            return Result<UserDto>.Fail(IdentityResult.Errors.Select(E => Error.Validation(E.Code, E.Description)).ToList());
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim(JwtRegisteredClaimNames.Name,user.UserName!)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SecretKey = _configuration["JwtOptions:SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: _configuration["JwtOptions:Issuer"],
                audience: _configuration["JwtOptions:Audience"],
                expires: DateTime.UtcNow.AddHours(1),
                claims: Claims,
                signingCredentials: Creds
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
    #endregion
}
