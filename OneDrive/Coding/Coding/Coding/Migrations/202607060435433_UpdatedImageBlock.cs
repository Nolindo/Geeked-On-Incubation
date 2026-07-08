namespace Coding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedImageBlock : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Galleries", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Galleries", "FileName", c => c.String(nullable: false));
        }
    }
}
