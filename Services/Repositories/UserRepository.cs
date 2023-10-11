﻿using UserAPI.Models;
using UserAPI.Services.Interfaces;

namespace UserAPI.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
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

        public async Task<User?> GetUser(JwtUser requestJwt)
        {
            User? resultUser = (await new DBManager("DEV").ExecuteSPAsync<User>("dbo.spGetUser", new
            {
                email = requestJwt.Email,
                password = requestJwt.Password
            })).ToList().FirstOrDefault();

            return resultUser;
        }
        
        public async Task<List<User>> GetUsers(User requestUser)
        {
            List<User> user_list = new List<User>();

            user_list = (await new DBManager("DEV").ExecuteSPAsync<User>("dbo.spGetAllUsers", null)).ToList();

            return user_list;
        }

        public async Task<bool> NewUser(User user)
        {
            var result = (await new DBManager("DEV").ExecuteSPAsync<bool>("dbo.spNewUser", new
            {
                name = user.Name,
                email = user.Email,
                password = user.Password
            }));
            return true;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var result = (await new DBManager("DEV").ExecuteSPAsync<bool>("dbo.spDeleteUser", new
            {
                UserId = userId
            }));
            return true;
        }
    }
}