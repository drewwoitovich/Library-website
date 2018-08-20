using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace c_final_capstone_v2.Models
{
    public class PollResults
    {
        public string Username { get; set; }
        public int FavoriteBook { get; set; }
        public int FavoriteAuthors { get; set; }
        public DateTime WeekOf { get; set; }
    }
}