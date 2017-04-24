namespace Cynfo1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMembershipType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        name = c.String(),
                        adsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "MembershipTypeId", c => c.Byte(nullable: true));
            CreateIndex("dbo.AspNetUsers", "MembershipTypeId");
            AddForeignKey("dbo.AspNetUsers", "MembershipTypeId", "dbo.MembershipTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "MembershipTypeId", "dbo.MembershipTypes");
            DropIndex("dbo.AspNetUsers", new[] { "MembershipTypeId" });
            DropColumn("dbo.AspNetUsers", "MembershipTypeId");
            DropTable("dbo.MembershipTypes");
        }
    }
}
