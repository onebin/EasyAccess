namespace EasyAccess.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NonNullableInt = c.Int(nullable: false),
                        NonNullableDecimal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NonNullableFloat = c.Single(nullable: false),
                        NonNullableDouble = c.Double(nullable: false),
                        NonNullableByte = c.Byte(nullable: false),
                        NonNullableString = c.String(),
                        NonNullableDateTime = c.DateTime(nullable: false),
                        NonNullableSexEnum = c.Int(nullable: false),
                        NullableInt = c.Int(),
                        NullableDecimal = c.Decimal(precision: 18, scale: 2),
                        NullableFloat = c.Single(),
                        NullableDouble = c.Double(),
                        NullableByte = c.Byte(),
                        NullableSexEnum = c.Int(),
                        NullableDateTime = c.DateTime(),
                        _RowVersion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Account", "_RowVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Account", "_RowVersion", c => c.Int(nullable: false));
            DropTable("dbo.Test");
        }
    }
}
