using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using c_final_capstone_v2.Models;

namespace c_final_capstone_v2.DAL
{
    public class PollSqlDAO : IPollSqlDAO
    {
        private string connectionString;
        private static string sqlListAllBooks = @"SELECT DISTINCT (title + ' By: ' + authors) AS title_and_authors, book_id FROM book ORDER BY title_and_authors;";
        private static string sqlListAllAuthors = @"SELECT DISTINCT authors, book_id FROM book ORDER BY authors;";
        private static string sqlGetPollResults = @"SELECT TOP 1 week_of, (SELECT TOP 1 b.authors FROM poll p " +
        "JOIN book b ON p.book_id_for_favorite_authors = b.book_id " +
        "GROUP BY week_of, book_id_for_favorite_authors, b.authors " +
        "ORDER BY week_of DESC, count(*) DESC) AS favorite_authors, " +
        "(SELECT TOP 1 b.title FROM poll p JOIN book b ON p.book_id_for_favorite_title = b.book_id " +
        "GROUP BY week_of, book_id_for_favorite_title, b.title " +
        "ORDER BY week_of DESC, count(*) DESC) AS favorite_book " +
        "FROM poll p ORDER BY week_of DESC;";
        private static string sqlCreatePoll = @"INSERT INTO poll (username, book_id_for_favorite_title, book_id_for_favorite_authors, week_of) VALUES (@username, @favoriteBook, @favoriteAuthors, @weekOf);";



        public PollSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Dictionary<string, int> ListAllBooks()
        {
            Dictionary<string, int> allBooks = new Dictionary<string, int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlListAllBooks, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        allBooks[Convert.ToString(reader["title_and_authors"])] = Convert.ToInt32(reader["book_id"]);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return allBooks;
        }

        public Dictionary<string, int> ListAllAuthors()
        {
            Dictionary<string, int> allAuthors = new Dictionary<string, int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlListAllAuthors, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        allAuthors[Convert.ToString(reader["authors"])] = Convert.ToInt32(reader["book_id"]);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return allAuthors;
        }

        public List<PollResultsModel> GetPollResults()
        {
            List<PollResultsModel> allPolls = new List<PollResultsModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlGetPollResults, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PollResultsModel pollResult = new PollResultsModel();

                        //pollResult.Username = Convert.ToString(reader["username"]);
                        pollResult.FavoriteBook = Convert.ToString(reader["favorite_book"]);
                        pollResult.FavoriteAuthors = Convert.ToString(reader["favorite_authors"]);
                        pollResult.WeekOf = (Convert.ToDateTime(reader["week_of"])).ToString("d");

                        allPolls.Add(pollResult);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return allPolls;
        }

        public bool CreatePoll(CreatePollModel poll)
        {
            bool wasAdded = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlCreatePoll, conn);
                    cmd.Parameters.AddWithValue("@username", poll.Username);
                    cmd.Parameters.AddWithValue("@favoriteBook", poll.FavoriteBook);
                    cmd.Parameters.AddWithValue("@favoriteAuthors", poll.FavoriteAuthors);
                    cmd.Parameters.AddWithValue("@weekOf", poll.WeekOf);
                    
                    if (cmd.ExecuteNonQuery() > 0)
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