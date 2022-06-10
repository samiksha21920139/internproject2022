using CLMS.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CLMS.Models;

namespace CLMS.Models
{
    public class RequestBook
    {
        [Key]
        public int RequestId { get; set; }
        public string BookTitle { get; set; }
        public int BookId { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> BorrowedDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> ReturnDate { get; set; }
       

    }
}