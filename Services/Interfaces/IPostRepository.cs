using fsmAPI.Models;

namespace fsmAPI.Services.Interfaces
{
    public interface IPostRepository
    {
        public Task<bool> CreatePost();
        public Task<List<Post>> GetPostsByAuthor(int authorId);
    }
}
