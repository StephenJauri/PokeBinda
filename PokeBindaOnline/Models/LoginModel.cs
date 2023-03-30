using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PokeBindaOnline.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please input your email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please input your password")]
        [MinLength(8)]
        public string Password { get; set; }
    }
}