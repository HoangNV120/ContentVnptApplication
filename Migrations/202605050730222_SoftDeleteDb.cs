namespace ContentVnptApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SoftDeleteDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostCategories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PostCategories", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Posts", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Posts", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "DeletedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DeletedAt");
            DropColumn("dbo.Users", "IsDeleted");
            DropColumn("dbo.Posts", "DeletedAt");
            DropColumn("dbo.Posts", "IsDeleted");
            DropColumn("dbo.PostCategories", "DeletedAt");
            DropColumn("dbo.PostCategories", "IsDeleted");
        }
    }
}
