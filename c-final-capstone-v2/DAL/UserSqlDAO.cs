using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using c_final_capstone_v2.Models;

namespace c_final_capstone_v2.DAL
{
    public class UserSqlDAO : IUserSqlDAO
    {
        private static string sqlCreateUser = "INSERT INTO [dbo].[user] " +
        "([username], [password], [last_search], [is_admin], [newsletter] " +
        ",[email]) VALUES (@username, @password, NULL, @isAdmin, " +
        "@newsletter, @email)";

        private string connectionString;

        public UserSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CreateUser(User newUser)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlCreateUser, conn);
                    cmd.Parameters.AddWithValue("@username", newUser.Username);
                    cmd.Parameters.AddWithValue("@password", newUser.Password);
                    cmd.Parameters.AddWithValue("@isAdmin", newUser.IsAdmin);
                    cmd.Parameters.AddWithValue("@newsletter", newUser.Newsletter);
                    cmd.Parameters.AddWithValue("@email", newUser.Email);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception)
            {
                 return false;
            }
        }

    }
}