namespace BM4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "DateOfRegistration", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.UserProfiles", "UserName", c => c.String());
            DropColumn("dbo.UserProfiles", "FBUserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "FBUserName", c => c.String());
            DropColumn("dbo.UserProfiles", "UserName");
            DropColumn("dbo.UserProfiles", "DateOfRegistration");
        }
    }
}
