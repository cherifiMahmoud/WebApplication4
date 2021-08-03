using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.Core.ApiModes;
using WebApplication4.Core.Interfaces;
using WebApplication4.Core.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IJwtService jwtService)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._jwtService = jwtService;
        }

        [HttpPost(nameof(CheckEmailExists))]
        public async Task<ActionResult<ApiResponse<CheckEmailApiModel.Response>>>  CheckEmailExists(CheckEmailApiModel.Request request)
        {
            var response = new ApiResponse<CheckEmailApiModel.Response>();
            response.Data = new CheckEmailApiModel.Response();

            response.Data.Exists = await (checkEmail(request.Email));
           
            return Ok(response);
        }

        [HttpPost(nameof(SignUp))]
        public async Task<ActionResult<ApiResponse<SignUpApiModel.ResponseSingUp>>> SignUp(SignUpApiModel.RequestSignUp request)
        {
            var response = new ApiResponse<SignUpApiModel.ResponseSingUp>();

            if (await checkEmail(request.Email))
            {
                response.AddError(1);
                return response;    
            }

            var appUser = new AppUser()
            {
                Email = request.Email,
                UserName = request.Email

            };

            var result = await _userManager.CreateAsync(appUser, request.Password);
            if (result.Succeeded == false)
            {
                //ToDO;
                return NotFound();
            }

            response.Data = new SignUpApiModel.ResponseSingUp()
            {
                Email=request.Email
            };
            return Ok(response);

        }

        [HttpPost(nameof(SignIn))]
        public async Task<ActionResult<ApiResponse<SignInApiModel.ResponseSingIn>>> SignIn(SignInApiModel.RequestSignIn requestSignIn)
        {
            var response = new ApiResponse<SignInApiModel.ResponseSingIn>();

            var user = await _userManager.FindByEmailAsync(requestSignIn.Email);
            
            if (user==null)
            {
                response.AddError(2);
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user, requestSignIn.Password, false, false);
            
            if (result.Succeeded == false)
            {
                response.AddError(3);
                return response;
            }

            response.Data = new SignInApiModel.ResponseSingIn();

            response.Data.Token = _jwtService.GenerateAccessToken(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "")
            });

            return Ok(response);
        }


        #region private Helper
        private async Task<bool> checkEmail(string email)
        {
            if (await _userManager.FindByEmailAsync(email) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        } 
        #endregion
    }
}
