﻿using WhatShouldIDoNow.DataAccess.Models;

namespace WhatShouldIDoNow.DataAccess
{
    public interface IUserQueries
    {
        User GetUserByUserName(string userName);
        string GetPasswordHashByUserName(string userName);
        User GetUserById(int id);
        bool GetWhetherUsernameExists(string username);
        bool GetWhetherEmailExists(string email);
    }
}
