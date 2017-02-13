namespace Cynfo1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingAdsModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "ExpirationDate", c => c.DateTime());
            AddColumn("dbo.Advertisements", "isExpired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Advertisements", "isExpired");
            DropColumn("dbo.Advertisements", "ExpirationDate");
        }
    }
}
