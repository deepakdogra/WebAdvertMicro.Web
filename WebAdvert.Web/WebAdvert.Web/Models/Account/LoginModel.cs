using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebAdvert.Web.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Email is requried")]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Password is requried")]
        [DataType(DataType.Password)]
        [StringLength(8, ErrorMessage = "Password must be 8 Characters long")]
        public String Password { get; set; }
      
        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
