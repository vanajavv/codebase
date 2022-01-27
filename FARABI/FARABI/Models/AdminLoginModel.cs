using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FARABI.Models
{
    public class AdminLoginModel
    {
        public string name { get; set; }
        [Display(Name = "User Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Email Id Required")]

        public string EmailId { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool Rememberme { get; set; }
    }
}