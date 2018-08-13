using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using c_final_capstone_v2.Models;
using System.Data.SqlClient;

namespace c_final_capstone_v2.DAL
{
    public class BookSqlDAO : IBookSqlDAO
    {
        private static string sqlTitleSearch = "SELECT title, authors, genre, shelf_number " +
        "from book WHERE title LIKE '%@searchValue%'";

        private static string sqlAddBook = "INSERT INTO [dbo].[book] " +
        "([authors], [title], [genre], [shelf_number], [add_date] " +
        ",[email]) VALUES (@username, @password, NULL, @isAdmin, " +
        "@newsletter, @email)";

        private string connectionString;

        public BookSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Book> SearchByTitle(string searchValue)
        {
            List<Book> searchResults = new List<Book>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlTitleSearch, conn);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);


                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Book book = new Book();

                        Author a = new Author();
                        a.FirstName = Convert.ToString(reader["first_name"]);
                        a.LastName = Convert.ToString(reader["last_name"]);

                        book.Author = a.FirstName + " " + a.LastName;
                        book.AddDate = Convert.ToDateTime(reader["add_date"]);
                        book.BookId = Convert.ToInt32(reader["book_id"]);
                        book.Genre = Convert.ToString(reader["genre"]);
                        book.ShelfNumber = Convert.ToInt32(reader["shelf_number"]);
                        book.Title = Convert.ToString(reader["title"]);

                        searchResults.Add(book);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return searchResults;
        }

        public bool AddBook(Book newBook)
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