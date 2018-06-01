namespace LiveMusicLovers.Web.UI.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class OverrideTheClassNameGenre : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Genroes", newName: "Genres");
            RenameColumn(table: "dbo.Gigs", name: "Genro_Id", newName: "Genre_Id");
            RenameIndex(table: "dbo.Gigs", name: "IX_Genro_Id", newName: "IX_Genre_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Gigs", name: "IX_Genre_Id", newName: "IX_Genro_Id");
            RenameColumn(table: "dbo.Gigs", name: "Genre_Id", newName: "Genro_Id");
            RenameTable(name: "dbo.Genres", newName: "Genroes");
        }
    }


}
