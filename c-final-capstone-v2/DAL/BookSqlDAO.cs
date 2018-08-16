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
        private static string sqlAddBook = "INSERT INTO [dbo].[book] " +
        "([authors], [title], [genre], [shelf_number], [add_date]" +
        ") VALUES (@authors, @title, @genre, @shelfNumber, " +
        "@addDate)";

        private static string sqlMasterSearch = "SELECT * FROM book WHERE " +
            "authors LIKE @author AND title LIKE @title AND genre LIKE @genre";

        private static string sqlMasterSearchNewBook = "SELECT * FROM book WHERE " +
           "authors LIKE @author AND title LIKE @title AND genre LIKE @genre AND add_date >= @lastUserSearch";


        private string connectionString;
        // Constructor
        public BookSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Takes a new book as an argument and adds that book to the
        // book table in the connected database
        public bool AddBook(Book newBook)
        {
            bool wasAdded = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlAddBook, conn);
                    cmd.Parameters.AddWithValue("@authors", newBook.Author);
                    cmd.Parameters.AddWithValue("@title", newBook.Title);
                    cmd.Parameters.AddWithValue("@genre", newBook.Genre);
                    cmd.Parameters.AddWithValue("@shelfNumber", newBook.ShelfNumber);
                    cmd.Parameters.AddWithValue("@addDate", DateTime.Now);

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

        // Searches DB based on any combination of title, author or genre
        public List<Book> MasterSearch(string titleInput, string authorInput, string genreInput)
        {
            List<Book> searchResults = new List<Book>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlMasterSearch, conn);
                    cmd.Parameters.AddWithValue("@author", $"%{authorInput}%");
                    cmd.Parameters.AddWithValue("@title", $"%{titleInput}%");
                    cmd.Parameters.AddWithValue("@genre", $"%{genreInput}%");

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Book book = new Book();

                        book.Author = Convert.ToString(reader["authors"]);
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

        // Searches for new books based on any combination of title author or genre
        public List<Book> MasterSearchNewBooks(DateTime lastUserSearch, string titleInput, string authorInput, string genreInput)
        {
            List<Book> searchResults = new List<Book>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlMasterSearchNewBook, conn);
                    cmd.Parameters.AddWithValue("@author", $"%{authorInput}%");
                    cmd.Parameters.AddWithValue("@title", $"%{titleInput}%");
                    cmd.Parameters.AddWithValue("@genre", $"%{genreInput}%");
                    cmd.Parameters.AddWithValue("@lastUserSearch", lastUserSearch.Date);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Book book = new Book();

                        book.Author = Convert.ToString(reader["authors"]);
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