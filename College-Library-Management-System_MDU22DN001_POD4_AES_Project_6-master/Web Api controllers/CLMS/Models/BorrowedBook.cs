using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CLMS.Models;

namespace CLMS.Database
{
    public class BorrowedBook
    {
        [Key]
        public int BorrowId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> BorrowedDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> ReturnDate { get; set; }
        public int Penalty { get; set; }
        public string BookTitle { get; set; }
        public int BookId { get; set; }
    }
}