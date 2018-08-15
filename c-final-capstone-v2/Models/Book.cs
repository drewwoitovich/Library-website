using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace c_final_capstone_v2.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        [Display(Name = "Shelf Number")]
        public int ShelfNumber { get; set; }

        [Display(Name = "Add Date")]
        public DateTime AddDate { get; set; }
    }
}