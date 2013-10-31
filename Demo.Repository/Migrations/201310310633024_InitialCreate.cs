namespace Demo.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Memo = c.String(maxLength: 255),
                        Location = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DataCollection",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        Value = c.String(maxLength: 255),
                        Subject_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subject", t => t.Subject_Id, cascadeDelete: true)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.ArticleConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SectionConfig",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ParentId = c.Int(),
                        Name = c.String(maxLength: 255),
                        Depth = c.Int(nullable: false),
                        Index = c.Int(nullable: false),
                        IsRepeatable = c.Boolean(nullable: false),
                        Article_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SectionConfig", t => t.ParentId)
                .ForeignKey("dbo.ArticleConfig", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.Article_Id);
            
            CreateTable(
                "dbo.InputConfig",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        InputType = c.Int(nullable: false),
                        IsRequired = c.Boolean(nullable: false),
                        RegExp = c.String(maxLength: 512),
                        DefaultValue = c.String(maxLength: 255),
                        Memo = c.String(),
                        Tips = c.String(maxLength: 255),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SectionConfig", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.InputConfig", new[] { "SectionId" });
            DropIndex("dbo.SectionConfig", new[] { "Article_Id" });
            DropIndex("dbo.SectionConfig", new[] { "ParentId" });
            DropIndex("dbo.DataCollection", new[] { "Subject_Id" });
            DropForeignKey("dbo.InputConfig", "SectionId", "dbo.SectionConfig");
            DropForeignKey("dbo.SectionConfig", "Article_Id", "dbo.ArticleConfig");
            DropForeignKey("dbo.SectionConfig", "ParentId", "dbo.SectionConfig");
            DropForeignKey("dbo.DataCollection", "Subject_Id", "dbo.Subject");
            DropTable("dbo.InputConfig");
            DropTable("dbo.SectionConfig");
            DropTable("dbo.ArticleConfig");
            DropTable("dbo.DataCollection");
            DropTable("dbo.Subject");
        }
    }
}
