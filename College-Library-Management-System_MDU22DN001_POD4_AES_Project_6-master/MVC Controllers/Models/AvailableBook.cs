using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace College_library_management_system.Models
{
    public class AvailableBook
    {
        [Key]
        public int BookId { get; set; }
        public string Category { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Count { get; set; }

        // public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; }
        //public virtual ICollection<RequestBook> RequestBooks { get; set; }


    }
}