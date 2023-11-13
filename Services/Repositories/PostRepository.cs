using fsmAPI.Models;
using fsmAPI.Services.Interfaces;

namespace fsmAPI.Services.Repositories
{
    public class PostRepository : IPostRepository
    {
        public PostRepository()
        {
        
        }

        public Task<bool> CreatePost()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetPostsByAuthor(int authorId)
        {
            List<Post> post_list = new List<Post>();

            post_list = (await new DBManager("DEV").ExecuteSPAsync<Post>("dbo.spGetPostsByUserId", new
            {
                id = authorId
            })).ToList();

            return post_list;
        }
    }
}
