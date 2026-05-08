namespace ContentVnptApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBaseModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banners", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.PostCategories", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.PostCategories", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Posts", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "IsBanned", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "DeletedAt", c => c.DateTime());
            DropColumn("dbo.Banners", "Created");
            DropColumn("dbo.Posts", "Created");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Banners", "Created", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "DeletedAt");
            DropColumn("dbo.AspNetUsers", "IsDeleted");
            DropColumn("dbo.AspNetUsers", "UpdatedAt");
            DropColumn("dbo.AspNetUsers", "CreatedAt");
            DropColumn("dbo.AspNetUsers", "IsBanned");
            DropColumn("dbo.Posts", "UpdatedAt");
            DropColumn("dbo.Posts", "CreatedAt");
            DropColumn("dbo.PostCategories", "UpdatedAt");
            DropColumn("dbo.PostCategories", "CreatedAt");
            DropColumn("dbo.Banners", "CreatedAt");
        }
    }
}
