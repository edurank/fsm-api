using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using fsmAPI.Models;
using fsmAPI.Services.Interfaces;

namespace fsmAPI.Controllers
{

    [ApiController]
    [Route("login")]
    public class AccountController : ControllerBase
    {
        private IAccountRepository accountRepository = null;
        private IConfiguration configuration = null;

        public AccountController(ILogger<AccountController> logger, IAccountRepository _accountRepository, IConfiguration _configuration)
        {
            this.accountRepository = _accountRepository;
            this.configuration = _configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] JwtUser jwtUser)
        {
            UserLogin registeredUser = null;

            registeredUser = await accountRepository.Login(jwtUser);

            if (registeredUser == null)
            {
                return Unauthorized();
            }
            return Ok(generateToken(registeredUser));
        }

        private string generateToken(UserLogin user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
            );

            var subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            });

            var expires = DateTime.UtcNow.AddMinutes(10);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
