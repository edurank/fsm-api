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
        
        [HttpGet("")]
        public async Task<IActionResult> GetPostsByUser()
        {
            int userId = getUserId();
            if (userId == -1) return Unauthorized();

            User requestUser = new User()
            {
                Id = getUserId()
            };
          
            
            List<Post> posts = await postRepository.GetPostsByAuthor(requestUser);
            return Ok(posts);
        }
        
        [HttpGet("popular")]
        public async Task<IActionResult> GetPosts()
        {

            throw new NotImplementedException();
            /*int userId = getUserId();
            if (userId == -1) return Unauthorized();

            User requestUser = new User()
            {
                Id = getUserId()
            };


            List<Post> posts = await postRepository.GetAllPosts();
            return Ok(posts);*/
        }

        [HttpPost]
        public async Task<IActionResult> NewPost([FromBody] NewPost request)
        {
            int userId = getUserId();
            if (userId == -1) return Unauthorized();

            request.AuthorId = userId;
            
            bool success = await postRepository.NewPost(request);
            return Ok(success);
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
