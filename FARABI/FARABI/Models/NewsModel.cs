using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FARABI.Models
{
    public class NewsModel
    {
        [Required(ErrorMessage = "New Title Required")]
        public string Title { get; set; }
        [Required(ErrorMessage ="News Description Cannot be Blank")]
        public string Description { get; set; }
        [Required(ErrorMessage ="News Date REquired")]
        public string Date { get; set; }
        [Required (ErrorMessage ="Select News Priority status")]
        public string Status { get; set; }
        public string Photos { get; set; }
    }
}