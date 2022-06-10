
using CLMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CLMS.Database
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