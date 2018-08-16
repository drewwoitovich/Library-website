/////In IBookSqlDAO:

public List<string> GetAllGenres();

/////In BookSqlDAO:
private static string sqlGetAllGenres = @"SELECT DISTINCT genre FROM book GROUP BY genre;";

public List<string> GetAllGenres()
{
    List<string> allGenresList = new List<string>();

    try
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sql_GetAllGenres, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                allGenresList.Add(Convert.ToString(reader["genre"]));
            }
        }
    }
    catch (SqlException ex)
    {
        throw;
    }

    return allGenresList;
}


/////In Search controller:

public ActionResult Search()
{
    ViewBag.Genres = bookDAO.GetAllGenres();
    return View();
}

/////In Search view:

@{
    List<SelectListItem> genreList = new List<SelectListItem>();
    foreach (string genre in ViewBag.Genres)
    {
        SelectListItem genre = new SelectListItem { Text = genre, Value = genre };
        genreList.Add(genre);
    }
}

@using(...)
{
    <div class="formValidationGroup form-group">
        @Html.DropDownListFor(m => m.GenreSearchInput, genreList, new { placeholder = "Genre", @class = "form-control" })
    </div>
}