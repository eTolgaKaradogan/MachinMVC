namespace _03_DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class o2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Mail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Mail");
        }
    }
}
