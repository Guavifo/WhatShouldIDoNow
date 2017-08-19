using WhatShouldIDoNow.DataAccess.Models;

namespace WhatShouldIDoNow.DataAccess
{
    public interface IUserQueries
    {
        User GetUserByUserName(string userName);
        string GetPasswordHashByUserName(string userName);
        User GetUserById(int id);
    }
}
