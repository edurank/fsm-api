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

        public async Task<List<Post>> GetPostsByAuthor(User user)
        {
            List<Post> post_list = new List<Post>();

            post_list = (await new DBManager("DEV").ExecuteQueryAsync<Post>("dbo.spGetPostsByUserId", new
            {
                id = user.Id
            })).ToList();

            return post_list;
        }

        public async Task<bool> NewPost(NewPost newPost)
        {
            bool success = false;
            
            
            success = await new DBManager("DEV").ExecuteScalarAsync<bool>("dbo.spInsertPost", new
            {
                authorId = newPost.AuthorId,
                content = newPost.Content
            });
            
            return success;
        }
    }
}
