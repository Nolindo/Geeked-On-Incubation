namespace Coding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Project : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageTitle = c.String(nullable: false),
                        ImageDescription = c.String(nullable: false),
                        FileName = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Collections", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Galleries", "CategoryId", "dbo.Collections");
            DropIndex("dbo.Galleries", new[] { "CategoryId" });
            DropTable("dbo.Galleries");
            DropTable("dbo.Collections");
        }
    }
}
