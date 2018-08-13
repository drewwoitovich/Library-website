using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c_final_capstone_v2.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<Author> Authors { get; set; }
        public string Genre { get; set; }
    }
}