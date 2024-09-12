using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
//using MySql.Data.MySqlClient;

namespace BookstoreManagement
{
    /// <summary>
    /// 此类维护数据库连接字符串，和 Connection 对象
    /// </summary>
  public class DBHelper
    {
        //// 数据库连接字符串
        //private string connString = @"Database=bookdb;Data Source=127.0.0.1;User Id=root;Password=root;pooling=false;CharSet=utf8;port=3306";

        //// 数据库连接 Connection 对象
        //private MySqlConnection connection;

        ///// <summary>
        ///// Connection对象
        ///// </summary>
        //public MySqlConnection Connection
        //{
        //    get
        //    {
        //        if (connection == null)
        //        {
        //            connection = new MySqlConnection(connString);
        //        }
        //        return connection;
        //    }            
        //}

        ///// <summary>
        ///// 打开数据库连接
        ///// </summary>
        //public void OpenConnection()
        //{
        //    if (Connection.State == ConnectionState.Closed)
        //    {
        //        Connection.Open();
        //    }
        //    else if (Connection.State == ConnectionState.Broken)
        //    {
        //        Connection.Close();
        //        Connection.Open();
        //    }
        //}

        ///// <summary>
        ///// 关闭数据库连接
        ///// </summary>
        //public void CloseConnection()
        //{
        //    if (Connection.State == ConnectionState.Open || Connection.State == ConnectionState.Broken)
        //    {
        //        Connection.Close();
        //    }
        //}
    }
}
