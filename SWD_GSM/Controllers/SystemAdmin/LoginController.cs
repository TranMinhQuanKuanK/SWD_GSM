using BusinessLayer.RequestModels.CreateModels;
using BusinessLayer.ResponseModels.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace SWD_GSM.Controllers.SystemAdmin
{
    [Route(SystemAdminRoute)]
    [ApiController]
    [ApiExplorerSettings(GroupName = Role)]
    public class LoginController : BaseSystemAdminController
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            if (login.Username==config["AdminAccount:Username"] && 
                login.Password == config["AdminAccount:Password"])
            {
                //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));
                //var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                //var tokenOptions = new JwtSecurityToken(
                //    issuer: "http://localhost:2000",
                //    audience: "http://localhost:2000",
                //    claims: new List<Claim>() {
                //    new Claim(ClaimTypes.Role, Role)
                //    },
                //    expires: DateTime.Now.AddDays(7),
                //    signingCredentials: signinCredentials
                //); ;

                //var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                string tokenString = CreateAuthenToken.GetToken(Role);
                return Ok(new {Token = tokenString});
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
