using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared.DTOS.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthenticationController (IServiceManager _serviceManager):ControllerBase
    {
        //Login Post//BaseUrl/api/Authentication/Login

        [HttpPost ("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        { 
          var user=await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(user);
        }


        //Login Post//BaseUrl/api/Authentication/Register

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }


    }




}
