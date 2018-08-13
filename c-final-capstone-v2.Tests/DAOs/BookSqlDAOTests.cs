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


namespace c_final_capstone_v2.Tests.DAOs
{
    class BookSqlDAOTests
    {
        public string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=library;Integrated Security=True";
        [TestClass]
        public class BookSqlDAOTests
        {


            [TestMethod]
            public void BookSqlDAOSearchByTitle()
            {
                // Arrange
                BookSqlDAO testDAO = new BookSqlDAO(connectionString);

                // Act
                List<Book> testResults;
                testResults = testDAO.SearchByTitle("Star Wars");

                // Assert
                Assert.AreEqual(testResults[1], "Star Wars");
            }
        }
    }
}
