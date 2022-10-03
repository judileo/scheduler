using scheduler.core.Auth;
using scheduler.core.Dtos.Requests;
using scheduler.core.Dtos.Responses;
using scheduler.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.core.Interfaces
{
    public interface IIdentityService
    {
        public Task<AuthenticationResult> RegisterAsync(UserRegisterDto dto);
        public Task<AuthenticationResult> LoginAsync(UserLoginDto dto);
        public List<GetUserResponseDto> GetAll();
    }
}
