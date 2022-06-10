namespace CLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updats : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestBooks", "UserName", c => c.String());
            AlterColumn("dbo.BorrowedBooks", "BorrowedDate", c => c.DateTime());
            AlterColumn("dbo.BorrowedBooks", "ReturnDate", c => c.DateTime());
            AlterColumn("dbo.RequestBooks", "BorrowedDate", c => c.DateTime());
            AlterColumn("dbo.RequestBooks", "ReturnDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RequestBooks", "ReturnDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RequestBooks", "BorrowedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BorrowedBooks", "ReturnDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BorrowedBooks", "BorrowedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.RequestBooks", "UserName");
        }
    }
}
