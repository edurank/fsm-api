using fsmAPI.Services.Interfaces;
using fsmAPI.Models;

namespace UserAPI.Services.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository()
        {

        }

        // Public Methods
        public async Task<User> Login(JwtUser user)
        {
          List<User> userResponse = null;
          userResponse = (await new DBManager("DEV").ExecuteSPAsync<User>("dbo.spGetUser", new {
                email = user.Email,
                password = user.Password
                })).ToList();
          return userResponse.FirstOrDefault();
        }
    }
}