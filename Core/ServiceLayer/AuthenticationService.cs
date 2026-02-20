using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using ServiceAbstractionLayer;
using Shared.DTOS.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager)
        : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            //Chaeck if Email Exist
            var user= await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UserNotFoundException(loginDto.Email);
            //Chaeck Pass
            var isPassValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (isPassValid)
            {
                //Return UserDto
                return new UserDto()
                { 
                  Email = loginDto.Email!,
                  DisplayName=user.DispalyName,
                  Token= CreateTokenAsync(user)

                };
            }
            else throw new UnauthorizedException();
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            //Convert Dto TO Entity
            var user = new ApplicationUser()
            {
                DispalyName = registerDto.Email,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };
            var res= await _userManager.CreateAsync(user,registerDto.Password );
            if (res.Succeeded) return new UserDto()
            {
                DisplayName = user.DispalyName,
                Email = user.Email,
                Token = CreateTokenAsync(user)
            };
            else
            {
                var errors = res.Errors.Select(e=>e.Description).ToList();
                throw new BadRequestException(errors);
            }
           //CreateAsync
        }

        private string CreateTokenAsync(ApplicationUser user)
        {
            return "Taken"; 
        }
    }
}
