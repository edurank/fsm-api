using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using fsmAPI.Models;
using fsmAPI.Services.Interfaces;

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

        [HttpPost]
        public async Task<IActionResult> User([FromBody] JwtUser request) {
            // Get logged in user.
            var result = await userRepository.GetUser(request);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] User user)
        {
            bool isDeleted = await userRepository.DeleteUser(user.Id);
            return Ok(isDeleted);
        }

        
        private int getUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            string subClaim = identity?.FindFirst("sub").Value;

            return int.Parse(subClaim);
        }
    }
}
