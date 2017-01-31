namespace Cynfo1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@" 
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'400cbaf4-82c3-4f3a-b33a-28822644376f', N'admin@cynfo.com', 0, N'AMjWvRb3IZhw2m4pY5At2cKkNw7MTdpU4kK15G/pX0xzay/dv0tLSWOtmV6JWHHJ/Q==', N'da673c54-fbb1-470e-b020-03d515f84b57', NULL, 0, 0, NULL, 1, 0, N'admin@cynfo.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c03ec7da-84cd-4d69-91e5-66d191120568', N'guest@cynfo.com', 0, N'AEkUw/GUC/Ad6QYynW12JQguw4uony3T36Nn8lwe0OVnBNvgvz6yE1FYriCLuPpudA==', N'c067c5c2-19ae-4d18-8681-b2c7b70cb2c7', NULL, 0, 0, NULL, 1, 0, N'guest@cynfo.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'5097615a-8a41-4f8b-8239-fd6d64d9803f', N'GeneralAdmin')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'400cbaf4-82c3-4f3a-b33a-28822644376f', N'5097615a-8a41-4f8b-8239-fd6d64d9803f')

                    ");
        }
        
        public override void Down()
        {
        }
    }
}
