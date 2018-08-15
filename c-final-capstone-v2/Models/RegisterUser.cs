using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace c_final_capstone_v2.Models
{
    public class RegisterUser
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Please enter a password that is at least 8 characters.")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Subscribe to our newsletter")]
        public bool Newsletter { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}