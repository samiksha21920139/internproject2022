using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace College_library_management_system.Models
{
    public class UserTable
    {
        [Key]
        public int UserId { get; set; }
        public string UserRole { get; set; }
        public string UserName { get; set; }
        public string Department { get; set; }
        public int Year { get; set; }


    }
}