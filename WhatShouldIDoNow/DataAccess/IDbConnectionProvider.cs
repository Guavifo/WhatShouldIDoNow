using System.Data;

namespace WhatShouldIDoNow.DataAccess
{
    public interface IDbConnectionProvider
    {
        IDbConnection GetOpenWsidnConnection();
    }
}
