using System;
using System.Threading.Tasks;

namespace WhatShouldIDoNow.Services
{
    public interface ISecurityService
    {
        Task SignIn(int userId);
        Task SignOut();
        PasswordStatus VerifyUserPassword(string username, string password);
        int GetCurrentUserId();
        SignedInUser GetSignedInUser();
        void UpdateUserPassword(string username, string newPassword);
    }
}
