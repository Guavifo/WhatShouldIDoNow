namespace WhatShouldIDoNow.Services
{
    public interface IUserSignUpService
    {
        int SignUpUser(string email, string username, string password);
        bool IsUsernameAvailable(string username);
        bool IsEmailAvailable(string email);
    }
}
