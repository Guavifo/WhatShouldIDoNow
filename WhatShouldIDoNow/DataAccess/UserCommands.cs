using Dapper;

namespace WhatShouldIDoNow.DataAccess
{
    public class UserCommands : IUserCommands
    {
        private IDbConnectionProvider _connectionProvider;

        public UserCommands(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public int CreateUser(string email, string username, string hash)
        {
            var sql = @"
                    insert into Users
                    (UserName, Email, PasswordHash)
                    values (@username, @email, @hash)
                    select SCOPE_IDENTITY()";

            using (var conn = _connectionProvider.GetOpenWsidnConnection())
            {
                var id = conn.ExecuteScalar<int>(sql, new { email, username, hash });
                return id;
            }
        }

        public void UpdatePasswordHashByUsername(string username, string hash)
        {
            var sql = @"
                update Users
                set PasswordHash = @hash
                where UserName = @username";

            using (var conn = _connectionProvider.GetOpenWsidnConnection())
            {
                conn.Execute(sql, new { username, hash });
            }
        }
    }
}
