using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Dto_s.IdentityDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation.Controllers
{

 
    public class AuthenticationController(IServicesManger servicesManger) :ApiBaseController
    {

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await servicesManger.AuthenticationServices.LoginAsync(loginDto);
            return Ok(user);
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto  registerDto)
        {
            var user = await servicesManger.AuthenticationServices.RegisterAsync(registerDto);
            return Ok(user);
        }



    }
}
