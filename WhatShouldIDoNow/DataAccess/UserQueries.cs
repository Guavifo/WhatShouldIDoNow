﻿using System.Linq;
using WhatShouldIDoNow.DataAccess.Models;
using Dapper;
using System;

namespace WhatShouldIDoNow.DataAccess
{
    public class UserQueries: IUserQueries
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public UserQueries(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public User GetUserByUserName(string userName)
        {
            var query = @"
                select top 1
                    Id, Username, Email, PasswordHash 
                from 
                    Users
                where 
                    Username = @username";

            using (var conn = _dbConnectionProvider.GetOpenWsidnConnection())

            {
                var user = conn.Query<User>(query, new { userName });
                return user.FirstOrDefault();
            }
        }

        public string GetPasswordHashByUserName(string userName)
        {
            var query = @"
                select top 1
                    PasswordHash
                from
                    Users
                where
                    UserName = @userName";
            using (var conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                var passwordHash = conn.Query<string>(query, new { userName });
                return passwordHash.FirstOrDefault();
            }
        }

        public User GetUserById(int id)
        {
            var query = @"
                select top 1
                    Id, Username, Email, PasswordHash 
                from 
                    Users
                where 
                    Id = @id";

            using (var conn = _dbConnectionProvider.GetOpenWsidnConnection())
            {
                var user = conn.Query<User>(query, new { id });
                return user.FirstOrDefault();
            }
        }
    }
}
