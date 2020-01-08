namespace BM4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "DateOfBirth", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
