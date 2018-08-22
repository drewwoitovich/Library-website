using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using c_final_capstone_v2.Models;
using System.Data.SqlClient;

namespace c_final_capstone_v2.DAL
{
    public class ForumPostSqlDAO : IForumPostSqlDAO
    {
        private static string sqlGetAllForumPosts = @"SELECT [username], [message] FROM forum ORDER BY post_id DESC;";

        private static string sqlCreateForumPost = @"INSERT INTO[dbo].[forum] ([username], [message]) VALUES (@username, @message);";

        private string connectionString;

        // Constructor
        public ForumPostSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<ForumPost> GetAllForumPosts()
        {
            List<ForumPost> allPosts = new List<ForumPost>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetAllForumPosts, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ForumPost post = new ForumPost();

                        post.Username = Convert.ToString(reader["username"]);
                        post.Message = Convert.ToString(reader["message"]);


                        allPosts.Add(post);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return allPosts;
        }

        public bool CreatePost(ForumPost post)
        {
            bool wasAdded = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlCreateForumPost, conn);
                    cmd.Parameters.AddWithValue("@username", post.Username);
                    cmd.Parameters.AddWithValue("@message", post.Message);

                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        wasAdded = true;
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return wasAdded;
        }
    }
}