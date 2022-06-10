namespace CLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SignIns", "loginErrorMessage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SignIns", "loginErrorMessage");
        }
    }
}
