namespace ContentVnptApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Author = c.String(),
                        IsPublished = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                        PostCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PostCategories", t => t.PostCategoryId, cascadeDelete: true)
                .Index(t => t.PostCategoryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "PostCategoryId", "dbo.PostCategories");
            DropIndex("dbo.Posts", new[] { "PostCategoryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Posts");
            DropTable("dbo.PostCategories");
        }
    }
}
