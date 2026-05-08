namespace ContentVnptApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cloudinary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banners", "PublicId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Banners", "PublicId");
        }
    }
}
