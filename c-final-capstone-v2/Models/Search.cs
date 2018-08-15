using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace c_final_capstone_v2.Models
{
    public class Search
    {
        public string AuthorSearchInput { get; set; }
        public string TitleSearchInput { get; set; }
        public string GenreSearchInput { get; set; }
        public DateTime DateTimeSearchInput { get; set; }
        [Display(Name = "Search for only new books")]
        public bool NewSearch { get; set; }
    }
}