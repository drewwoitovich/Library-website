using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using c_final_capstone_v2;
using c_final_capstone_v2.DAL;
using c_final_capstone_v2.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;


namespace c_final_capstone_v2.Tests.DAOs
{
    [TestClass]
    public class BookSqlDAOTests
    {
        // public string connectionString = ConfigurationManager.ConnectionStrings["libraryConnection"].ConnectionString;
        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=library;Integrated Security=True";

        private TransactionScope scope;
        private IBookSqlDAO dao;

        [TestInitialize]
        public void Initializer()
        {
            scope = new TransactionScope();
            dao = new BookSqlDAO(connectionString);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT book (title, authors, genre, shelf_number, add_date)" +
                                                "VALUES ('Star Wars', 'John Fulton', 'Test Genre', 2, '2018-08-14');", conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            scope.Dispose();
        }


        [TestMethod]
        public void BookSqlDAOAddBookTest()
        {
            // Arrange
            BookSqlDAO testDAO = new BookSqlDAO(connectionString);
            Book testBook = new Book();
            testBook.Author = "Alyson Wood";
            testBook.Title = "Turtles";
            testBook.Genre = "Non-fiction";
            testBook.ShelfNumber = 7;

            // Act
            bool didWork = testDAO.AddBook(testBook);
            List<Book> testList = testDAO.MasterSearch("", "Alyson", "");

            // Assert
            Assert.AreEqual(true, didWork);
            Assert.AreEqual("Alyson Wood", testList[0].Author);
        }

        [TestMethod]
        public void MasterSearchTest()
        {
            // Arrange
            BookSqlDAO testDAO = new BookSqlDAO(connectionString);

            // Act
            List<Book> testResults = new List<Book>();
            testResults = testDAO.MasterSearch("Star", "John", "Test");

            // Assert
            Assert.AreEqual(2, testResults.Count);
            Assert.AreEqual("John Fulton", testResults[0].Author);
        }


        [TestMethod]
        public void MasterSearchNewBookTest()
        {
            // Arrange
            BookSqlDAO testDAO = new BookSqlDAO(connectionString);

            // Act
            List<Book> testResults = new List<Book>();
            testResults = testDAO.MasterSearchNewBooks(Convert.ToDateTime("2018-06-30"), "Star", "", "");

            // Assert
            Assert.AreEqual(2, testResults.Count);
            Assert.AreEqual("John Fulton", testResults[0].Author);
        }
    }
}

