namespace Cynfo1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMediaType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Advertisements", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Advertisements", "MediaURL", c => c.String(nullable: false));
            AlterColumn("dbo.Beacons", "MACAddress", c => c.String(nullable: false));
            DropColumn("dbo.Advertisements", "MediaType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Advertisements", "MediaType", c => c.Byte(nullable: false));
            AlterColumn("dbo.Beacons", "MACAddress", c => c.String());
            AlterColumn("dbo.Advertisements", "MediaURL", c => c.String());
            AlterColumn("dbo.Advertisements", "Title", c => c.String());
        }
    }
}
