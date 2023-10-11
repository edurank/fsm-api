using fsmAPI.Models;

namespace fsmAPI.Services.Interfaces
{
    public interface IAccountRepository
    {
        public Task<User> Login(JwtUser jwtUser);
    }
}
