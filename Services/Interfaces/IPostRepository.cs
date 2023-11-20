using fsmAPI.Models;

namespace fsmAPI.Services.Interfaces
{
    public interface IPostRepository
    {
        public Task<bool> CreatePost();
        public Task<List<Post>> GetPostsByAuthor(User user);
        public Task<bool> NewPost(NewPost newPost);
    }
}
