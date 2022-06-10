using CLMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CLMS.Database
{
    public class LibraryContext:DbContext
    {
        public LibraryContext():base("LibraryDbConnection"){
            base.Configuration.ProxyCreationEnabled = false;
        }
        public virtual DbSet<UserTable> UserTables { get; set; }
        public virtual DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public virtual DbSet<AvailableBook> AvailableBooks { get; set; }
        public virtual DbSet<SignIn> SignIns { get; set; }
        public virtual DbSet<RequestBook> RequestBooks { get; set; }
    }
}