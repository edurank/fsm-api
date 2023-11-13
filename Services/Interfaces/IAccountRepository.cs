using fsmAPI.Models;

namespace fsmAPI.Services.Interfaces
{
    public interface IAccountRepository
    {
        public Task<UserLogin> Login(JwtUser jwtUser);
    }
}
