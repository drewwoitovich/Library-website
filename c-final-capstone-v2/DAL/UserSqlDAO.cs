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

        private static string sqlGetDateOfLastSearch = "SELECT last_search " +
            "from [user] where user_id = @UserId";

        private static string sqlUpdateUsersLastSearch = "UPDATE [dbo].[user] " +
            "SET[last_search] = @DateTimeNow WHERE user_id = @UserId";

        private string connectionString;
        // Constructor
        public UserSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Takes in a new user as an argument and adds them to
        // the users table in the connected database
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

        // Takes in a user and updates their last_search field
        // in the database to the current date and time
        public bool UpdateUsersLastSearch(User user)
        {
            bool wasUpdated = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlUpdateUsersLastSearch, conn);
                    cmd.Parameters.AddWithValue("@DateTimeNow", Convert.ToString(DateTime.Now));
                    cmd.Parameters.AddWithValue("@UserId", user.UserId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        wasUpdated = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return wasUpdated;
        }

    }
}