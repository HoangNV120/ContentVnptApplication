namespace ContentVnptApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSlugPótCategoryDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostCategories", "Slug", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostCategories", "Slug");
        }
    }
}
