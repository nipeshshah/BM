namespace BM4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendConnections",
                c => new
                    {
                        FriendConnectionId = c.Int(nullable: false, identity: true),
                        UserId1 = c.String(maxLength: 128),
                        UserId2 = c.String(maxLength: 128),
                        Status = c.String(),
                        ConnectedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FriendConnectionId)
                .ForeignKey("dbo.UserProfiles", t => t.UserId1)
                .ForeignKey("dbo.UserProfiles", t => t.UserId2)
                .Index(t => t.UserId1)
                .Index(t => t.UserId2);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        ProfilePic = c.String(),
                        City = c.String(),
                        ReferralCode = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        MainLocationId = c.Int(nullable: false),
                        LocationTypes = c.String(),
                        Text1 = c.String(),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("dbo.MainLocations", t => t.MainLocationId, cascadeDelete: true)
                .Index(t => t.MainLocationId);
            
            CreateTable(
                "dbo.MainLocations",
                c => new
                    {
                        MainLocationId = c.Int(nullable: false, identity: true),
                        City = c.String(nullable: false),
                        Text2 = c.String(),
                        Text3 = c.String(),
                        Text4 = c.String(),
                    })
                .PrimaryKey(t => t.MainLocationId);
            
            CreateTable(
                "dbo.LocationTypes",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        CitySearchbox = c.String(nullable: false),
                        LocationTypes = c.String(),
                        Standard = c.String(),
                        SchoolName = c.String(),
                        EducationBoard = c.String(),
                        SemesterYear = c.String(),
                        CourseFaculty = c.String(),
                        College = c.String(),
                        University = c.String(),
                        Department = c.String(),
                        Company = c.String(),
                        Batches = c.String(),
                        Subject = c.String(),
                        Classes = c.String(),
                        Professor = c.String(),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserConnections",
                c => new
                    {
                        ConnectionId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        AppType = c.String(nullable: false),
                        AppUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ConnectionId)
                .ForeignKey("dbo.UserProfiles", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserEvents",
                c => new
                    {
                        UserEventId = c.Int(nullable: false, identity: true),
                        LocationId = c.Int(nullable: false),
                        StartingDate = c.DateTime(nullable: false),
                        EndingDate = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserEventId)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.LocationId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserEvents", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserEvents", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.UserConnections", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Locations", "MainLocationId", "dbo.MainLocations");
            DropForeignKey("dbo.FriendConnections", "UserId2", "dbo.UserProfiles");
            DropForeignKey("dbo.FriendConnections", "UserId1", "dbo.UserProfiles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.UserEvents", new[] { "UserId" });
            DropIndex("dbo.UserEvents", new[] { "LocationId" });
            DropIndex("dbo.UserConnections", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Locations", new[] { "MainLocationId" });
            DropIndex("dbo.FriendConnections", new[] { "UserId2" });
            DropIndex("dbo.FriendConnections", new[] { "UserId1" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserEvents");
            DropTable("dbo.UserConnections");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LocationTypes");
            DropTable("dbo.MainLocations");
            DropTable("dbo.Locations");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.FriendConnections");
        }
    }
}
