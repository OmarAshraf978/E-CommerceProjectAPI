using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Shared.Dtos.IdentityDtos;
using ECommerce.Shared.ResultPattern;

namespace ECommerce.Services_Abstraction.IServices
{
    public interface IAuthenticationService
    {
        public Task<Result<UserDto>> LoginAsync(LoginDto loginDto);
        public Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto);
        public Task<bool> CheckEmailAsync(string email);
        public Task<Result<UserDto>> GetUserByEmailAsync(string email);
    }
}
