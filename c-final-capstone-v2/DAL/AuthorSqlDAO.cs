﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using c_final_capstone_v2.Models;
using System.Data.SqlClient;

namespace c_final_capstone_v2.DAL
{
    public class AuthorSqlDAO : IAuthorSqlDAO
    {
        private static string sqlAuthorSearch = "SELECT title, authors, genre, shelf_number " +
            "from book WHERE authors LIKE '%@searchValue%'";

        private string connectionString;
        // Constructor
        public AuthorSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Takes user input and searches the book table for any
        // boot title that contains that user input
        public List<Book> SearchByAuthor(string searchValue)
        {
            List<Book> searchResults = new List<Book>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlAuthorSearch, conn);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);


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