using UserAPI.Models;

namespace UserAPI.Services.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUsers(User requestUser);
        public Task<User?> GetUser(JwtUser requestJwt);
        public Task<bool> NewUser(User user);
        public Task<bool> DeleteUser(int userId);
        public Task<User> Login(JwtUser jwtUser);
    }
}
