﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using WhatShouldIDoNow.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WhatShouldIDoNow.Services
{
    public class SecurityService : ISecurityService
    {
        private HttpContext _httpContext;
        private IUserQueries _userQueries;
        private const string CLAIM_TYPE_ID = "id";

        public SecurityService(IHttpContextAccessor httpContextAccessor, IUserQueries userQueries)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _userQueries = userQueries;
        }

        public async Task SignIn(int userId)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(CLAIM_TYPE_ID, userId.ToString()));

            var identity = new ClaimsIdentity(
                claims, 
                CookieAuthenticationDefaults.AuthenticationScheme);

            await _httpContext.Authentication.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(identity));
        }

        public async Task SignOut()
        {
            await _httpContext.Authentication
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool VerifyUserPassword(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var retrievedPassword = _userQueries.GetPasswordHashByUserName(userName);

            return password == retrievedPassword;
        }

        public int GetCurrentUserId()
        {
            var userId = _httpContext
                .User
                .Claims
                .FirstOrDefault(x => x.Type == CLAIM_TYPE_ID)
                ?.Value;

            int id;
            int.TryParse(userId, out id);
            return id;   
        }

        public SignedInUser GetSignedInUser()
        {
            var id = GetCurrentUserId();
            var user = _userQueries.GetUserById(id);

            if (user == null)
            {
                return null;
            }

            var signedInUser = new SignedInUser
            {
                Id = user.Id,
                UserName = user?.UserName
            };

            return signedInUser;
        }
    }
}
