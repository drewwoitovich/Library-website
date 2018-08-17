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
        "([username], [password], [is_admin], [newsletter], [email]) " +
        "VALUES (@username, @password, NULL, @newsletter, @email)";

        private static string sqlGetUserNames = @"SELECT username FROM [user];";

        private static string sqlGetUser = @"SELECT TOP 1 * FROM [user] WHERE username = @username;";

        private static string sqlAddToReadingList = "INSERT INTO [dbo].[reading_list] " +
            "([username], [book_id], [read_status]) VALUES (@username, @bookId, 0)";

        private static string sqlGetReadingList = "SELECT rl.username, rl.read_status, b.title, b.authors, b.genre, b.shelf_number, b.book_id"
                                                   + " FROM reading_list rl"
                                                   + " JOIN book b ON b.book_id = rl.book_id"
                                                   + " WHERE rl.username = @username"
                                                   + " ORDER BY rl.read_status";

        private static string sqlMarkAsRead = "UPDATE[dbo].[reading_list] " +
        "SET[read_status] = 1 WHERE username = @username AND book_id = @bookId";

        private static string sqlDeleteFromReadingList = " DELETE " +
        "FROM reading_list WHERE book_id = @bookId AND username = @username";

        private static string sqlCheckReadingListAvailability = " SELECT COUNT(book_id) AS quantity " +
        "FROM reading_list WHERE book_id = @bookId AND username = @username";

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
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetUserNames, conn);
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

        public User GetUser(string username)
        {
            User user = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetUser, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User
                        {
                            Username = Convert.ToString(reader["username"]),
                            Password = Convert.ToString(reader["password"]),
                            IsAdmin = Convert.ToBoolean(reader["is_admin"])
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

        public bool AddToReadingList(string username, int bookId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlAddToReadingList, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@bookId", bookId);

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

        public bool CheckReadingListAvailability(string username, int bookId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlCheckReadingListAvailability, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@bookId", bookId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        if(Convert.ToInt32(reader["quantity"]) >= 1)
                        {
                            return false;
                        }
                    }
                     
                    return true;
                }
            }
            catch(Exception ex)
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
                            BookId = Convert.ToInt32(reader["book_id"])
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

        public bool MarkAsRead(string username, int bookId)
        {
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlMarkAsRead, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@bookId", bookId);

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

        public bool DeleteFromReadingList(string username, int bookId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlDeleteFromReadingList, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@bookId", bookId);

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
    }
}