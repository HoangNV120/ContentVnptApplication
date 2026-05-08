namespace ContentVnptApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSlugDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Slug", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Slug");
        }
    }
}
