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
        public void BookSqlDAOSearchByTitleTest()
        {
            // Arrange
            BookSqlDAO testDAO = new BookSqlDAO(connectionString);

            // Act
            List<Book> testResults = new List<Book>();
            testResults = testDAO.SearchByTitle("Star Wars");

            // Assert
            Assert.AreEqual(3, testResults.Count);
            Assert.AreEqual("Star Wars", testResults[0].Title);
        }

        [TestMethod]
        public void BookSqlDAOSearchByAuthorTest()
        {
            // Arrange
            BookSqlDAO testDAO = new BookSqlDAO(connectionString);

            // Act
            List<Book> testResults = new List<Book>();
            testResults = testDAO.SearchByAuthor("John");

            // Assert
            Assert.AreEqual(1, testResults.Count);
            Assert.AreEqual("John Fulton", testResults[0].Author);
        }
    }
}

