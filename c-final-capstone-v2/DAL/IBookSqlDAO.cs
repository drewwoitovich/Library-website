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
        List<Book> SearchByTitle(string searchValue);

        List<Book> SearchByAuthor(string searchValue);

        bool AddBook(Book newBook);
    }
}
