using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace c_final_capstone_v2.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } 
        
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}