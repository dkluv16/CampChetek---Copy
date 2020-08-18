using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CampChetek.Models
{
    public class User
    {
        public int userId { get; set; }

        [Required(ErrorMessage = "Please enter a First Name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a Last Name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter an Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a Password.")]
        [MaxLength(25)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        public string UserEmail { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
