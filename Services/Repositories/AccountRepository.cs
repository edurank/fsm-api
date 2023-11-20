using fsmAPI.Services.Interfaces;
using fsmAPI.Models;

namespace fsmAPI.Services.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository()
        {

        }

        // Public Methods
        public async Task<UserLogin> Login(JwtUser user)
        {
          List<UserLogin> userResponse = null;
          userResponse = (await new DBManager("DEV").ExecuteQueryAsync<UserLogin>("dbo.spGetUserLogin", new {
                email = user.Email,
                password = user.Password
                })).ToList();
          return userResponse.FirstOrDefault();
        }
    }
}