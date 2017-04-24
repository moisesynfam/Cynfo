namespace Cynfo1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMemTypeTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "Price", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "Price");
        }
    }
}
