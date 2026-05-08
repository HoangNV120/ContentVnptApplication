namespace ContentVnptApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBaseModelBanner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banners", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Banners", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Banners", "DeletedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Banners", "DeletedAt");
            DropColumn("dbo.Banners", "IsDeleted");
            DropColumn("dbo.Banners", "UpdatedAt");
        }
    }
}
