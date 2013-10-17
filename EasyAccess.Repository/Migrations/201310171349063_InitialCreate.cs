namespace EasyAccess.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Age = c.Int(nullable: false),
                        Sex = c.Int(nullable: false),
                        Memo = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Name_FirstName = c.String(maxLength: 50),
                        Name_LastName = c.String(maxLength: 50),
                        Name_NickName = c.String(maxLength: 50),
                        Contact_Email = c.String(maxLength: 255),
                        Contact_Phone = c.String(maxLength: 32),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Register", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Register",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LoginUser_UserName = c.String(nullable: false, maxLength: 50),
                        LoginUser_Password = c.String(nullable: false, maxLength: 255),
                        Salt = c.Guid(nullable: false),
                        LastLoginIP = c.String(maxLength: 32),
                        LastLoginTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        HomePage = c.String(),
                        Memo = c.String(),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MenuId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        ActionUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menu", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ParentId = c.String(maxLength: 128),
                        Name = c.String(),
                        System = c.String(),
                        Url = c.String(),
                        Depth = c.Int(nullable: false),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menu", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.RolePermission",
                c => new
                    {
                        RoleId = c.Long(nullable: false),
                        PermissionId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.PermissionId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Permission", t => t.PermissionId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.AccountRole",
                c => new
                    {
                        AccountId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountId, t.RoleId })
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.AccountRole", new[] { "RoleId" });
            DropIndex("dbo.AccountRole", new[] { "AccountId" });
            DropIndex("dbo.RolePermission", new[] { "PermissionId" });
            DropIndex("dbo.RolePermission", new[] { "RoleId" });
            DropIndex("dbo.Menu", new[] { "ParentId" });
            DropIndex("dbo.Permission", new[] { "MenuId" });
            DropIndex("dbo.Account", new[] { "Id" });
            DropForeignKey("dbo.AccountRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.AccountRole", "AccountId", "dbo.Account");
            DropForeignKey("dbo.RolePermission", "PermissionId", "dbo.Permission");
            DropForeignKey("dbo.RolePermission", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Menu", "ParentId", "dbo.Menu");
            DropForeignKey("dbo.Permission", "MenuId", "dbo.Menu");
            DropForeignKey("dbo.Account", "Id", "dbo.Register");
            DropTable("dbo.AccountRole");
            DropTable("dbo.RolePermission");
            DropTable("dbo.Menu");
            DropTable("dbo.Permission");
            DropTable("dbo.Role");
            DropTable("dbo.Register");
            DropTable("dbo.Account");
        }
    }
}
