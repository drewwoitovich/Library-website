using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c_final_capstone_v2.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime LastSearch { get; set; }
        public bool IsAdmin { get; set; }
        public bool Newsletter { get; set; }
        public string Email { get; set; }
    }
}