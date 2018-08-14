﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using c_final_capstone_v2.Models;
using System.Data.SqlClient;

namespace c_final_capstone_v2.DAL
{
    public class BookSqlDAO : IBookSqlDAO
    {
        string sqlTitleSearch = "SELECT book_id, title, authors, genre, shelf_number, add_date " +
               "from book WHERE title LIKE @searchValue";

        private static string sqlAuthorSearch = "SELECT book_id, title, authors, genre, shelf_number, " +
            " add_date from book WHERE authors LIKE @searchValue";

        private static string sqlAddBook = "INSERT INTO [dbo].[book] " +
        "([authors], [title], [genre], [shelf_number], [add_date]" +
        ") VALUES (@authors, @title, @genre, @shelfNumber, " +
        "@addDate)";

        private static string sqlNewBookSearch = "SELECT book_id, title, authors, genre, shelf_number, " +
            "add_date FROM book WHERE add_date > @UserLastSearch";

        private static string sqlMasterSearch = "SELECT * FROM book WHERE " +
            "authors LIKE @author AND title LIKE @title AND genre LIKE @genre @newOrNot";

        private string connectionString;
        // Constructor
        public BookSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Gets passed a search value from user and searches for any
        // titles that contain the given value
        public List<Book> SearchByTitle(string searchValue)
        {
            List<Book> searchResults = new List<Book>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlTitleSearch, conn);
                    cmd.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");

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

        // Searches the book table for any
        // book title that contains that user input
        public List<Book> SearchByAuthor(string searchValue)
        {
            List<Book> searchResults = new List<Book>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlAuthorSearch, conn);
                    cmd.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");


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

        // Returns a list of all books that have been added since 
        // the last time the user searched
        public List<Book> NewBookList(User user)
        {
            List<Book> listResults = new List<Book>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlAuthorSearch, conn);
                    cmd.Parameters.AddWithValue("@UserLastSearch", user.LastSearch);


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

                        listResults.Add(book);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return listResults;
        }


        public List<Book> UltimateMasterSearchMethodOfAllSearchMethods(User user, string titleInput, string authorInput, string genreInput, bool newOrNot)
        {
            List<Book> searchResults = new List<Book>();

            string newSearchString = "";

            if (newOrNot)
            {
                newSearchString = $"AND add_date > {user.LastSearch}";
            }
           
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlAuthorSearch, conn);
                    cmd.Parameters.AddWithValue("@author", $"%{authorInput}%");
                    cmd.Parameters.AddWithValue("@title", $"%{titleInput}%");
                    cmd.Parameters.AddWithValue("@genre", $"%{genreInput}%");
                    cmd.Parameters.AddWithValue("@newOrNot", newSearchString);

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