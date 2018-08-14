[TestClass]
public class BookSqlDAOTests
{
    private string connectionString = ConfigurationManager.ConnectionStrings["libraryConnection"].ConnectionString;
    private TransactionScope scope;
    private IBookSqlDAO dao;

    [TestInitialize]
    public void Initialize()
    {
        scope = new TransactionScope();
        dao = new BookSqlDAO(connectionString);

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT book (book_id, title, authors, genre, shelf_number, add_date)" +
                                            "VALUES (1, 'Star Wars', 'John Fulton', 'Test Genre', 2, DateTime.Now);", conn);
            cmd.ExecuteNonQuery();
        }
    }

    [TestMethod]
    public void SearchByTitleTest()
    {
        // Arrange
        // Act
        List<Book> testResults = dao.SearchByTitle("Star Wars");

        // Assert
        Assert.AreEqual(1, testResults.Count);
        Assert.AreEqual("Star Wars", testResults[0].Title);
    }

    [TestCleanup]
    public void Cleanup()
    {
        scope.Dispose();
    }
}