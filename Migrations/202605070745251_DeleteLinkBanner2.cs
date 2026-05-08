namespace ContentVnptApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteLinkBanner2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Banners", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Banners", "IsActive", c => c.Boolean(nullable: false));
        }
    }
}
