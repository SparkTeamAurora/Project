
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspireOverflow.Models;

using AspireOverflow.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;


using AspireOverflow.Services;
using System.ComponentModel.DataAnnotations;

namespace AspireOverflow.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly AspireOverflowContext _context;
        private ILogger<TokenController> _logger;
        private UserService _userService;
        public TokenController(IConfiguration config, UserService service, ILogger<TokenController> logger)
        {
            _configuration = config;
            _userService = service;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AuthToken(Login Crendentials)
        {

         
            try
            {
                if(!Validation.ValidateUserCredentials(Crendentials.Email,Crendentials.Password)) return BadRequest();
                var user = _userService.GetUser(Crendentials.Email, Crendentials.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Email,user.EmailAddress),
                        new Claim("UserId",user.UserId.ToString()),
                        new Claim(ClaimTypes.Role,user.UserRoleId.ToString())


                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                            claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);
                        var Result = new {
                            token=new JwtSecurityTokenHandler().WriteToken(token),
                        };
                    return Ok(Result);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            catch (ValidationException exception){
                  _logger.LogError(HelperService.LoggerMessage("TokenController", "AuthToken(String Email, string Password)", exception, Crendentials.Email));
                return BadRequest(exception.Message);

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("TokenController", "AuthToken(String Email, string Password)", exception, Crendentials.Email));
                return BadRequest("Error Occured while Validating your  credentials");
            }

        }


    }
}