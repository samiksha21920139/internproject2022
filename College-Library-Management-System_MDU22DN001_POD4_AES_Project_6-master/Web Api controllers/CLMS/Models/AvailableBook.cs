using CLMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CLMS.Database
{
    public class AvailableBook
    {
        [Key]
        public int BookId { get; set; }
        public string Category { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Count { get; set; }        
        
    }
}