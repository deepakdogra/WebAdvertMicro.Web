using System;
using System.ComponentModel.DataAnnotations;

namespace WebAdvert.Web.Models.Account
{
    public class SignUpModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Email Address")]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(8,ErrorMessage ="Password must be 8 Characters long")]
        public String Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, ErrorMessage = "Password doesn't match")]
        public String ConfirmPassword { get; set; }
    }
}
