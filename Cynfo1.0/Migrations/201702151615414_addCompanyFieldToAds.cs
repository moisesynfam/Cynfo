namespace Cynfo1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCompanyFieldToAds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "CompanyID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Advertisements", "CompanyID");
        }
    }
}
