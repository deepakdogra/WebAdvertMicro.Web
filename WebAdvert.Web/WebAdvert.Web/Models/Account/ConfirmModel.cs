using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebAdvert.Web.Models.Account
{
    public class ConfirmModel
    {
        [Required]
        [Display(Name = "Email Address")]
        public String Email { get; set; }

        [Required(ErrorMessage ="code is Required")]
        public String Code { get; set; }
    }
}
