using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace PhotoShare.Pages.Photgraphers.Models
{
    public class InputModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Full name")]
        public string Name { get; set; }

        
        [Display(Name = "Profile Picture")]
        public byte[]? UserPhoto { get; set; }

        [Required]
        [Display(Name = "Role")]
        [DataType(DataType.Text)]
        public string UserRoleToRegister { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }


        [Display(Name = "City")]
        public string City { get; set; }


        [Display(Name = "Years Of Expirience")]
        public int YearsOfExpirience { get; set; }

        [Display(Name = "Degree/Certificate")]
        public string Degree { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
