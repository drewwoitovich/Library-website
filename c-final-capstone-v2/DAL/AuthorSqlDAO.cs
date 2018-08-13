using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using c_final_capstone_v2.Models;
using System.Data.SqlClient;

namespace c_final_capstone_v2.DAL
{
    public class AuthorSqlDAO
    {
        private static string sqlAuthorSearch = "SELECT b.title, b.genre, b.shelf_number " +
            "a.first_name, a.last_name from book b JOIN author_book ab on ab.book_id " +
            "= b.book_id JOIN author a on ab.author_id = a.author_id WHERE a.last_name " +
            "LIKE '%@lastNameSearchValue%' AND a.first_name LIKE '%@firstNameSearchValue%'";

        private string connectionString;

        public AuthorSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Book> SearchByAuthor(string lastNameSearchValue, string firstNameSearchValue)
        {
            List<Book> searchResults = new List<Book>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlAuthorSearch, conn);
                    cmd.Parameters.AddWithValue("@lastNamesearchValue", lastNameSearchValue);
                    cmd.Parameters.AddWithValue("@firstNameSearchValue", firstNameSearchValue);


                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Book book = new Book();

                        Author a = new Author();
                        a.FirstName = Convert.ToString(reader["first_name"]);
                        a.LastName = Convert.ToString(reader["last_name"]);
                        book.Author = a;

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
    }
}