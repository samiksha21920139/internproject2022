using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace College_library_management_system.Models
{
    public class SignIn
    {
        [Key]
        [Required(ErrorMessage ="Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="User Id required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "User role required")]
        public string UserRole { get; set; }
        [Required(ErrorMessage = "User name required")]
        public string UserName { get; set; }
        public string LoginErrorMessage { get; set; }
    }
}