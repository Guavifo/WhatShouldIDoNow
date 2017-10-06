namespace WhatShouldIDoNow.DataAccess
{
    public interface IUserCommands
    {
        int CreateUser(string email, string username, string hash);
    }
}
