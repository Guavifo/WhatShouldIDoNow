using WhatShouldIDoNow.DataAccess;

namespace WhatShouldIDoNow.Services
{
    public class UserSignUpService
    {
        private readonly IUserQueries _userQueries;

        public UserSignUpService(IUserQueries userQueries)
        {
            _userQueries = userQueries;
        }

        public bool IsUsernameAvailable(string username)
        {
            return !_userQueries.GetWhetherUsernameExists(username);
        }

        public bool IsEmailAvailable(string email)
        {
            return !_userQueries.GetWhetherEmailExists(email);
        }
    }
}
