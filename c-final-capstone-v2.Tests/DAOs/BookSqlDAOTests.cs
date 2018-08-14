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
        public string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=library;Integrated Security=True";

        [TestInitialize]
        public void Initializer()
        {

        }

        [TestMethod]
        public void BookSqlDAOSearchByTitle()
        {
            // Arrange
            IBookSqlDAO testDAO = new BookSqlDAO(connectionString);

            // Act
            List<Book> testResults = new List<Book>();
            testResults = testDAO.SearchByTitle("Star Wars");

            // Assert
            Assert.AreEqual(1, testResults.Count);
            Assert.AreEqual("Star Wars", testResults[0].Title);
        }
    }
}
