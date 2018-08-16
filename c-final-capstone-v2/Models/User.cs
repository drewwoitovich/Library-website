using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace c_final_capstone_v2.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool Newsletter { get; set; }
        public string Email { get; set; }
        public List<Book> BooksToRead { get; set; }
        public List<Book> BooksRead { get; set; }
        public bool IsAdmin { get; set; }
    }
}