namespace LiveMusicLovers.Web.UI.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddImageColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "image", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "image");
        }
    }
}
