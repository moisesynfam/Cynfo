namespace Cynfo1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuickFixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beacons", "BussinessName", c => c.String());
            AddColumn("dbo.Beacons", "AreaId", c => c.Int(nullable: false));
            AddColumn("dbo.Beacons", "AreaName", c => c.String(nullable: false));
            AlterColumn("dbo.Advertisements", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Advertisements", "Description", c => c.String(maxLength: 255));
            DropColumn("dbo.Beacons", "Area");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Beacons", "Area", c => c.Int(nullable: false));
            AlterColumn("dbo.Advertisements", "Description", c => c.String());
            AlterColumn("dbo.Advertisements", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Beacons", "AreaName");
            DropColumn("dbo.Beacons", "AreaId");
            DropColumn("dbo.Beacons", "BussinessName");
        }
    }
}
