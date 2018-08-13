using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c_final_capstone_v2.Models
{
    public class Author
    {
        public string Name { get; set; }
        public List<Book> AuthorBooks { get; set; }
    }
}