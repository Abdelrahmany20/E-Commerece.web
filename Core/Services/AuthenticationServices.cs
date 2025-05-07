using Abstraction;
using AutoMapper;
using Domain.Exeptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Shared.Dto_s.IdentityDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationServices(UserManager<ApplicationUser> userManager) : IAuthenticationServices
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UserNotFoundExeption(loginDto.Email);



            var IsPasswordvalid  = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (IsPasswordvalid)
            {
                return new UserDto()
                {
                    DisplayName = user.DisplayName,

                    Email = user.Email,
                    Token = CreateTokenAsync(user)
                };


            }

            else
            {
                throw new unauthorizedException();
            }
        }






        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var User= new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
                DisplayName = registerDto.DisplayName
            };


            var Result = await userManager.CreateAsync(User, registerDto.Password);


            if (Result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = CreateTokenAsync(User)
                };
            }
            else
            {
               var Errors = Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestExeption(Errors);

            }
        }

        public static string CreateTokenAsync(ApplicationUser user)
        {

            return  "Token-ToDo";


        }
    }
}
