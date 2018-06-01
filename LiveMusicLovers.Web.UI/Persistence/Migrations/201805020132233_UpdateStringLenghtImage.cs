namespace LiveMusicLovers.Web.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStringLenghtImage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "image", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "image", c => c.String(maxLength: 10));
        }
    }
}
