using System.Data;
using System.Data.SqlClient;

namespace WhatShouldIDoNow.DataAccess
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        private string _wsidnConnectionString;

        public DbConnectionProvider(string wsidnConnectionString)
        {
            _wsidnConnectionString = wsidnConnectionString;
        }

        public IDbConnection GetOpenWsidnConnection()
        {
            var connection = new SqlConnection(_wsidnConnectionString);
            connection.Open();
            return connection;
        }
    }
}
