namespace EasyAccess.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Onebin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Account", "CreateTime");
        }
    }
}
