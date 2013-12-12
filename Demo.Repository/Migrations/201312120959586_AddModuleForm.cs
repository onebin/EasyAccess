namespace Demo.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddModuleForm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormConfig",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 255),
                    Memo = c.String(maxLength: 255),
                })
                .PrimaryKey(t => t.Id);

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
                    ArticleId = c.Int(nullable: false),
                    ParentId = c.Int(),
                    Name = c.String(maxLength: 255),
                    Depth = c.Int(nullable: false),
                    Index = c.Int(nullable: false),
                    IsRepeatable = c.Boolean(nullable: false),
                    TreeFlag = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SectionConfig", t => t.ParentId)
                .Index(t => t.ParentId);

            CreateTable(
                "dbo.InputConfig",
                c => new
                {
                    Id = c.Int(nullable: false),
                    InputType = c.Int(nullable: false),
                    IsRequired = c.Boolean(nullable: false),
                    ValidType = c.String(maxLength: 512),
                    DefaultValue = c.String(maxLength: 255),
                    Memo = c.String(),
                    Tips = c.String(maxLength: 255),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SectionConfig", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.DataCollection",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FormId = c.Int(nullable: false),
                    SectionId = c.Int(nullable: false),
                    GroupId = c.Int(nullable: false),
                    Value = c.String(maxLength: 255),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropIndex("dbo.InputConfig", new[] { "Id" });
            DropIndex("dbo.SectionConfig", new[] { "ParentId" });
            DropForeignKey("dbo.InputConfig", "Id", "dbo.SectionConfig");
            DropForeignKey("dbo.SectionConfig", "ParentId", "dbo.SectionConfig");
            DropTable("dbo.DataCollection");
            DropTable("dbo.InputConfig");
            DropTable("dbo.SectionConfig");
            DropTable("dbo.ArticleConfig");
            DropTable("dbo.FormConfig");
        }
    }
}
