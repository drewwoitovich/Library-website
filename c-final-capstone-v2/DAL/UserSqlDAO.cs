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
        "([username], [password], [is_admin], [newsletter] " +
        ",[email]) VALUES (@username, @password, NULL, " +
        "@newsletter, @email)";

        private static string sqlAddToReadingList = "INSERT INTO[dbo].[reading_list] " +
            "([username], [book_id], [read_status]) VALUES (@Username, @BookId " +
           ", 0)";

        private static string sqlGetReadingList = "SELECT rl.username, rl.read_status, b.title, b.authors, b.genre, b.shelf_number"
                                                   + " FROM reading_list rl"
                                                   + " JOIN book b ON b.book_id = rl.book_id"
                                                   + " WHERE rl.username = @username"
                                                   + " ORDER BY rl.read_status";

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
                    cmd.Parameters.AddWithValue("@newsletter", Convert.ToInt32(newUser.Newsletter));
                    cmd.Parameters.AddWithValue("@email", newUser.Email);

                    if(cmd.ExecuteNonQuery() >= 1)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetUsernames()
        {
            List<string> usernames = new List<string>();

            try
            {
                string sql = $"SELECT username FROM [user];";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        usernames.Add(Convert.ToString(reader["username"]));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return usernames;
        }

        public bool CheckUsernameAvailability(string username, List<string> allUsernames)
        {
            bool isAvailable = true;

            foreach (string takenUsername in allUsernames)
            {
                if (username.ToLower() == takenUsername.ToLower())
                {
                    isAvailable = false;
                }
            }
            return isAvailable;
        }

        //public User UserLogin(string username, string password)
        //{
        //    User u = new User();

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand(sqlLogin, conn);
        //            cmd.Parameters.AddWithValue("@username", username);
        //            cmd.Parameters.AddWithValue("@password", password);

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                u.UserId = Convert.ToInt32(reader["user_id"]);
        //                u.Username = Convert.ToString(reader["username"]);
        //                u.Email = Convert.ToString(reader["email"]);
        //                u.IsAdmin = Convert.ToBoolean(reader["is_admin"]);
        //                u.LastSearch = Convert.ToDateTime(reader["last_search"]);
        //                u.Password = Convert.ToString(reader["password"]);
        //                u.Newsletter = Convert.ToBoolean(reader["newsletter"]);

        //            }
        //        }
        //        return u;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public User GetUser(string username)
        {
            User user = null;

            try
            {
                string sql = $"SELECT TOP 1 * FROM [user] WHERE username = '{username}'";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User
                        {
                            Username = Convert.ToString(reader["username"]),
                            Password = Convert.ToString(reader["password"]),
                            IsAdmin = false
                        };
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return user;
        }

        public bool AddToReadingList(string username, int bookId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlAddToReadingList, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    if (cmd.ExecuteNonQuery() >= 1)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<List<Book>> GetReadingList(string username)
        {
            List<Book> read = new List<Book>();
            List<Book> notRead = new List<Book>();
            List<List<Book>> result = new List<List<Book>>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetReadingList, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Book b = new Book
                        {
                            Title = Convert.ToString(reader["title"]),
                            Author = Convert.ToString(reader["authors"]),
                            Genre = Convert.ToString(reader["genre"]),
                            ShelfNumber = Convert.ToInt32(reader["shelf_number"]),
                        };
                        bool readStatus = Convert.ToBoolean(reader["read_status"]);
                        if (readStatus)
                        {
                            read.Add(b);
                        }
                        else
                        {
                            notRead.Add(b);
                        }
                    }
                    result.Add(read);
                    result.Add(notRead);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}