using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using UserAPI.Services.Interfaces;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private IUserRepository userRepository = null;
        private IConfiguration configuration = null;

        public UserController(ILogger<UserController> logger, IUserRepository _userRepository, IConfiguration _configuration)
        {
            this.userRepository = _userRepository;
            this.configuration = _configuration;    
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetUsers()
        {
          User requestUser = new User()
          {
            Id = getUserId()
          };
            
          
            List<User> users = await userRepository.GetUsers(requestUser);
            return Ok(users);
        }

        [HttpPost("new")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            var response = await userRepository.NewUser(user);
            return Ok();
        }

        [HttpPost("")]
        public async Task<IActionResult> User([FromBody] JwtUser request) {
          // Get logged in user.
          var result = await userRepository.GetUser(request);
          
          return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] JwtUser jwtUser)
        {
          User registeredUser = null;
  
          registeredUser = await userRepository.Login(jwtUser);

          if(registeredUser == null)
          {
            return Unauthorized();
          }
          return Ok(generateToken(registeredUser));
        }

        [HttpDelete("")]
        public async Task<IActionResult> Delete([FromBody] User user)
        {
            var response = await userRepository.DeleteUser(user.Id);
            return Ok();
        }

        private string generateToken(User user)
        {
          var issuer = configuration["Jwt:Issuer"];
          var audience = configuration["Jwt:Audience"];
          var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
          var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature
              );

          var subject = new ClaimsIdentity(new []
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

        private int getUserId()
        {
          var identity = HttpContext.User.Identity as ClaimsIdentity;

          string subClaim = identity?.FindFirst("sub").Value;
          
          return int.Parse(subClaim);
        }
    }
}
