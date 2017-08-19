using System;
using System.Threading.Tasks;

namespace WhatShouldIDoNow.Services
{
    public interface ISecurityService
    {
        Task SignIn(int userId);
        Task SignOut();
        bool VerifyUserPassword(string username, string password);
        int GetCurrentUserId();
        SignedInUser GetSignedInUser();
    }
}
