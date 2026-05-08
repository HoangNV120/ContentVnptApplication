namespace ContentVnptApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteLinkBanner : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Banners", "Link");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Banners", "Link", c => c.String());
        }
    }
}
