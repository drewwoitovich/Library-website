using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace c_final_capstone_v2.Models
{
    public class Poll
    {
        public DateTime WeekOf { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string FavoriteBook { get; set; }

        [Required]
        public string FavoriteAuthors { get; set; }
    }
}