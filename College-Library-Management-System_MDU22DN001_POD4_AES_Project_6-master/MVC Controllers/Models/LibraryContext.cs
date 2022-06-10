using System;
using System.Data.Entity;
using System.Linq;

namespace College_library_management_system.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("name=LibraryContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }
        public virtual DbSet<UserTable> UserTables { get; set; }
      //  public virtual DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public virtual DbSet<AvailableBook> AvailableBooks { get; set; }
        public virtual DbSet<SignIn> SignIns { get; set; }
        public virtual DbSet<RequestBook> RequestBooks { get; set; }

    }
}