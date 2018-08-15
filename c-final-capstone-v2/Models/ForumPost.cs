using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace c_final_capstone_v2.Models
{
    public class ForumPost
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Message { get; set; }
    }
}