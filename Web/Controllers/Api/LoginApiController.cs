

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Web.ViewModel;

namespace Web.Controllers.Api;

[ApiController]
[Route("Api/Login")]
[AllowAnonymous]
public class LoginApiController : ControllerBase{
    public readonly IConfiguration _config;
    public LoginApiController(IConfiguration configuration) {
        _config = configuration;
    }
    

    [HttpPost]
    public async Task<IActionResult> GetToken([FromBody] LoginViewModel model) {
        
        if(model.User.Equals("potato") && model.Password.Equals("123")) {
            var claims = new[] {
                new Claim(ClaimTypes.Name, model.User)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["API_SECRET"])),
                    SecurityAlgorithms.HmacSha256
                )
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new {
                status= "Ok",
                msg = "Autenticación existosa",
                token= jwt
            });
        }

        return Unauthorized();

    }

}