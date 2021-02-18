namespace _03_DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class y5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hatırlatıcı", "To", c => c.String());
            AddColumn("dbo.Hatırlatıcı", "From", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hatırlatıcı", "From");
            DropColumn("dbo.Hatırlatıcı", "To");
        }
    }
}
