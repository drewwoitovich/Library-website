using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace c_final_capstone_v2.Models
{
    public class CreatePollModel
    {
        [Display (Name = "Week Of")]
        public DateTime WeekOf
        {
            get
            {
                int diff = (7 + (DateTime.Now.DayOfWeek - DayOfWeek.Monday)) % 7;
                return DateTime.Now.AddDays(-1 * diff).Date;
            }
        }

        [Required]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Favorite Book")]
        public string FavoriteBook { get; set; }

        [Required]
        [Display(Name = "Favorite Author(s)")]
        public string FavoriteAuthors { get; set; }
    }
}