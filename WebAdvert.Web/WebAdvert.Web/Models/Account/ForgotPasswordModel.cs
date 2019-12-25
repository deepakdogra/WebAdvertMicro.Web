using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebAdvert.Web.Models.Account
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [Display(Name ="Email Address")]
        public string Email { get; set; }
    }
}
