namespace CLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updats1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AvailableBooks", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AvailableBooks", "Count", c => c.String());
        }
    }
}
