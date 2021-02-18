namespace _03_DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hatırlatıcı",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Details = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        MachineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Machines", t => t.MachineId, cascadeDelete: true)
                .Index(t => t.MachineId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hatırlatıcı", "MachineId", "dbo.Machines");
            DropIndex("dbo.Hatırlatıcı", new[] { "MachineId" });
            DropTable("dbo.Hatırlatıcı");
        }
    }
}
