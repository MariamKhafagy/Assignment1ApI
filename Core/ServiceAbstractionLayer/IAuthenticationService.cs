using Shared.DTOS.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer
{
    public interface IAuthenticationService
    {
        //Login(Email , Password) -> Token ,Email , Display Name
        Task<UserDto> LoginAsync(LoginDto loginDto);

        //Regiater (Email , Password , ......) -> Email , Display Name
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
    }
}
