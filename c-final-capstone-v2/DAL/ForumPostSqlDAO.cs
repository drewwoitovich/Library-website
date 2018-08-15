using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using c_final_capstone_v2.Models;
using System.Data.SqlClient;

namespace c_final_capstone_v2.DAL
{
    public class ForumPostSqlDAO
    {

        private string connectionString;
        // Constructor
        public ForumPostSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private static string sqlGetAllForumPosts = "SELECT [username], [message] FROM forum";
           
        public List<ForumPost> GetAllFoumPosts()
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
    }
}