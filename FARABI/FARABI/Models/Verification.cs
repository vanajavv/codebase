using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FARABI.Models
{
    public class Verification
    {
        [Required(ErrorMessage = "Enter OTP")]
        public string OTP { get; set; }
    }
}