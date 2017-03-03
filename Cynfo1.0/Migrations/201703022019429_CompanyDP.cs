namespace Cynfo1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyDP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "COmpanyDpUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "COmpanyDpUrl");
        }
    }
}
