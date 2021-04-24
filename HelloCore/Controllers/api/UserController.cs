using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HelloCore.Areas.Identity.Data;
using HelloCore.Entities;
using HelloCore.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace HelloCore.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly UserManager<CustomUser> _userManager;
        private readonly AppSettings _appSettings;

        public UserController(SignInManager<CustomUser> signInManager, UserManager<CustomUser> userManager, IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("authenticate")]
        public async Task<object> Authenticate([FromBody] ApiUser apiUser)
        {
            SignInResult signInResult = await _signInManager.PasswordSignInAsync(apiUser.UserName, apiUser.Password, false, false);

            if (signInResult.Succeeded)
            {
                CustomUser customUser = _userManager.Users.SingleOrDefault(r => r.Email == apiUser.UserName);
                apiUser.Token = GenerateJwtToken(apiUser.UserName, customUser).ToString();

                return apiUser;
            }

            return BadRequest(new { message = "Username or password is incorrect" });
        }

        private string GenerateJwtToken(string email, CustomUser user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
