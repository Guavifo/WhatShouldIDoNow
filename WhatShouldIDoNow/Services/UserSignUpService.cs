using WhatShouldIDoNow.DataAccess;

namespace WhatShouldIDoNow.Services
{
    public class UserSignUpService : IUserSignUpService
    {
        private readonly IUserQueries _userQueries;
        private readonly IUserCommands _userCommands;
        private readonly IHashingWrapper _hashingWrapper;

        public UserSignUpService(
            IUserQueries userQueries, 
            IUserCommands userCommands,
            IHashingWrapper hashingWrapper)
        {
            _userQueries = userQueries;
            _userCommands = userCommands;
            _hashingWrapper = hashingWrapper;
        }

        public int SignUpUser(string email, string username, string password)
        {
            var hash = _hashingWrapper.HashPassword(password);
            var id = _userCommands.CreateUser(email, username, hash);
            return id;
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
