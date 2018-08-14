﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using c_final_capstone_v2.Models;

namespace c_final_capstone_v2.DAL
{
    public interface IBookSqlDAO
    {
        List<Book> MasterSearchNewBooks(DateTime lastUserSearch, string titleInput, string authorInput, string genreInput);

        List<Book> MasterSearch(string titleInput, string authorInput, string genreInput);

        bool AddBook(Book newBook);
    }
}
