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

        [HttpGet]
        public async Task<IActionResult> GetUser([FromBody] JwtUser request) {
            // Get logged in user.
            var result = await userRepository.GetUser(request);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("profile")]
        public async Task<IActionResult> GetProfile()
        {
            User profile = await userRepository.GetUserById(3);
            return Ok(profile);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] User user)
        {
            bool isDeleted = await userRepository.DeleteUser(user.Id);
            return Ok(isDeleted);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            bool isUpdated = await userRepository.UpdateUser(user.Id);
            return Ok(isUpdated);
        }
        
        // PRIVATE METHODS
        private int getUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            string subClaim = identity?.FindFirst("sub").Value;

            return int.Parse(subClaim);
        }
    }
}
