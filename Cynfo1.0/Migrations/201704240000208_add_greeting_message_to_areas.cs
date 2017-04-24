namespace Cynfo1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_greeting_message_to_areas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beacons", "GreetingMessage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Beacons", "GreetingMessage");
        }
    }
}
