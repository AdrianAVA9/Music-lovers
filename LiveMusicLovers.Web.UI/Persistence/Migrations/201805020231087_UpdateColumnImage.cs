namespace LiveMusicLovers.Web.UI.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateColumnImage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "image", c => c.String(maxLength: 50));
        }
    }
}
