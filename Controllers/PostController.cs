using fsmAPI.Models;
using fsmAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace fsmAPI.Controllers
{
    [ApiController]
    [Route("post")]
    public class PostController : ControllerBase
    {
        
        private IPostRepository postRepository = null;
        private IConfiguration configuration = null;

        public PostController(ILogger<PostController> logger, IPostRepository _postRepository, IConfiguration _configuration)
        {
            this.postRepository = _postRepository;
            this.configuration = _configuration;    
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetPosts()
        {
            int userId = getUserId();
            if (userId == -1) return Unauthorized();

            User requestUser = new User()
            {
                Id = getUserId()
            };
          
            
            List<Post> posts = await postRepository.GetPostsByAuthor(4);
            return Ok(posts);
        }

        // PRIVATE METHODS
        private int getUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int claim = -1;

            if (identity?.Claims.Count() == 0)          {
                return -1;
            }

            claim = int.Parse(identity.Claims.ToList()[0].Value);

            return claim; 
        }
    }
}
