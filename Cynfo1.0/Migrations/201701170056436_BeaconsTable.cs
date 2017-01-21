namespace Cynfo1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BeaconsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beacons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MACAddress = c.String(),
                        BussinessId = c.Int(nullable: false),
                        Area = c.Int(nullable: false),
                        AreaMediaUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Beacons");
        }
    }
}
