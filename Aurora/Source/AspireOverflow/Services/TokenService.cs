using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.Models;
using Microsoft.IdentityModel.Tokens;
namespace AspireOverflow.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly ILogger<TokenService> _logger;
        public TokenService(IConfiguration configuration, IUserService userService, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _userService = userService;
            _logger = logger;

        }


        public object GenerateToken(Login Credentials)
        {
            ValidateUser(Credentials);
            try
            {
                var user = _userService.GetUser(Credentials.Email!, Credentials.Password!);
                //create claims details based on the user information
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim(ClaimTypes.Email,user.EmailAddress),
                        new Claim("UserId",user.UserId.ToString()),
                        new Claim("RoleId",user.UserRoleId.ToString()),
                          new Claim(ClaimTypes.Role,user.UserRole?.RoleName!),
                        new Claim(ClaimTypes.Role, user.IsReviewer?"Reviewer":""),
                        new Claim("IsReviewer", user.IsReviewer.ToString())
                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var encryptingCredentials = new EncryptingCredentials(key, JwtConstants.DirectKeyUseAlg, SecurityAlgorithms.Aes256CbcHmacSha512);
                var token = new JwtSecurityTokenHandler().CreateJwtSecurityToken(
                  _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    new ClaimsIdentity(claims),
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    DateTime.Now,
                    signIn,
                    encryptingCredentials);
                var Result = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiryInMinutes = 30,
                    IsAdmin = user.UserRoleId == 1 ? "true" : "false",
                    IsReviewer = user.IsReviewer ? "true" : "false",
                    IsVerified = user.VerifyStatus?.Name
                };
                return Result;
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("TokenService", " GenerateToken(String Email, string Password)", exception, Credentials));
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("TokenService", " GenerateToken(String Email, string Password)", exception, Credentials));
                throw;
            }
        }


        private void ValidateUser(Login Credentials)
        {
            if (Credentials == null) throw new ArgumentException("Credentials cannot be null");
            Validation.ValidateUserCredentials(Credentials.Email!, Credentials.Password!);
        }
    }
}