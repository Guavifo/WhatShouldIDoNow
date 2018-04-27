using System.ComponentModel.DataAnnotations;

namespace WhatShouldIDoNow.Models
{
    public class SignUpViewModel
    {
        [Required, EmailAddress, StringLength(256)]
        public string Email { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required] 
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
