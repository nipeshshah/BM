namespace BM4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserEvents", "UserId", "dbo.UserProfiles");
            DropIndex("dbo.UserEvents", new[] { "UserId" });
            AlterColumn("dbo.UserProfiles", "DateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserProfiles", "DateOfRegistration", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserEvents", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserEvents", "UserId");
            AddForeignKey("dbo.UserEvents", "UserId", "dbo.UserProfiles", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserEvents", "UserId", "dbo.UserProfiles");
            DropIndex("dbo.UserEvents", new[] { "UserId" });
            AlterColumn("dbo.UserEvents", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserProfiles", "DateOfRegistration", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.UserProfiles", "DateOfBirth", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.UserEvents", "UserId");
            AddForeignKey("dbo.UserEvents", "UserId", "dbo.UserProfiles", "UserId", cascadeDelete: true);
        }
    }
}
