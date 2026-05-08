namespace ContentVnptApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostBrief : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PostBrief", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "PostBrief");
        }
    }
}
