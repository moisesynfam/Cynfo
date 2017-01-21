namespace Cynfo1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latestsofar : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Advertisements", "MediaURL", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Advertisements", "MediaURL", c => c.String(nullable: false));
        }
    }
}
