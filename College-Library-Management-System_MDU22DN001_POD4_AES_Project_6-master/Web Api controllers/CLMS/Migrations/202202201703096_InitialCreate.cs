namespace CLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvailableBooks",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        BookTitle = c.String(),
                        Author = c.String(),
                        Publisher = c.String(),
                        Count = c.String(),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.BorrowedBooks",
                c => new
                    {
                        BorrowId = c.Int(nullable: false, identity: true),
                        BorrowedDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        Penalty = c.Int(nullable: false),
                        BookTitle = c.String(),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BorrowId);
            
            CreateTable(
                "dbo.RequestBooks",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        BookTitle = c.String(),
                        BookId = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        BorrowedDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RequestId);
            
            CreateTable(
                "dbo.SignIns",
                c => new
                    {
                        Password = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                        UserRole = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Password);
            
            CreateTable(
                "dbo.UserTables",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserRole = c.String(),
                        UserName = c.String(),
                        Department = c.String(),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserTables");
            DropTable("dbo.SignIns");
            DropTable("dbo.RequestBooks");
            DropTable("dbo.BorrowedBooks");
            DropTable("dbo.AvailableBooks");
        }
    }
}
