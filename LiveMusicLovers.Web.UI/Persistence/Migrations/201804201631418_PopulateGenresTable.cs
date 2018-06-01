namespace LiveMusicLovers.Web.UI.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres(Id, Name) Values(1, 'Jazz')");
            Sql("INSERT INTO Genres(Id, Name) Values(2, 'BLue')");
            Sql("INSERT INTO Genres(Id, Name) Values(3, 'Country')");
            Sql("INSERT INTO Genres(Id, Name) Values(4, 'Rock')");
            Sql("INSERT INTO Genres(Id, Name) Values(5, 'Pop')");
            Sql("INSERT INTO Genres(Id, Name) Values(6, 'Electronic')");
            Sql("INSERT INTO Genres(Id, Name) Values(7, 'Salsa')");
        }

        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id IN (1,2,3,4,5,6,7)");
        }
    }
}
