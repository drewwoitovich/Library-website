using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace c_final_capstone_v2.Models
{
    public class PollResultsModel
    {
        public string Username { get; set; }

        [Display(Name = "Top Voted Book")]
        public string FavoriteBook { get; set; }

        [Display(Name = "Top Voted Authors")]
        public string FavoriteAuthors { get; set; }

        [Display(Name = "Week Of")]
        public string WeekOf { get; set; }
    }
}