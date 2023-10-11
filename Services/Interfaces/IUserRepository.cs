using fsmAPI.Models;

namespace fsmAPI.Services.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUsers(User requestUser);
        public Task<User?> GetUser(JwtUser requestJwt);
        public Task<bool> NewUser(User user);
        public Task<bool> DeleteUser(int userId);
    }
}
