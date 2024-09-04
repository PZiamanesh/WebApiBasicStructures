using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.Options;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationInfoOption _authenticationInfoOption;

        public class UserCredentials
        {
            public string? UserName { get; private set; }
            public string? Password { get; private set; }

            public UserCredentials(string? userName, string? password)
            {
                UserName = userName;
                Password = password;
            }
        }

        public class UserInfo
        {
            public int UserId { get; set; }
            public string UserName { get; set; }

            public UserInfo(int userId, string userName)
            {
                UserId = userId;
                UserName = userName;
            }
        }

        public AuthenticationController(IOptions<AuthenticationInfoOption> authenticationInfoOption)
        {
            _authenticationInfoOption = authenticationInfoOption.Value;
        }

        [HttpPost("authenticate")]
        public IActionResult AuthenticateUser(UserCredentials userCredentials)
        {
            var user = GetUser(userCredentials.UserName, userCredentials.Password);

            if (user is null)
            {
                return Unauthorized();
            }

            var userClaims = new List<Claim>()
            {
                new Claim("sub", user.UserId.ToString()),
                new Claim("userName", user.UserName)
            };
            byte[] secret = Encoding.UTF8.GetBytes(_authenticationInfoOption.Secret);
            var secretKey = new SymmetricSecurityKey(secret);
            var signingFunction = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var jwtTokenStructure = new JwtSecurityToken(
                _authenticationInfoOption.Issuer,
                _authenticationInfoOption.Audience,
                userClaims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingFunction
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtTokenStructure);

            return Ok(jwtToken);
        }

        private UserInfo GetUser(string? userName, string? password)
        {
            return new UserInfo(159, "ziasniper");
        }
    }
}
