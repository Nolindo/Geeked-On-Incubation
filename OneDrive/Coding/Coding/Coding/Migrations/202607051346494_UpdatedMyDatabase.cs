namespace Coding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedMyDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Galleries", "UploadDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Galleries", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Galleries", "IsActive");
            DropColumn("dbo.Galleries", "UploadDate");
        }
    }
}
