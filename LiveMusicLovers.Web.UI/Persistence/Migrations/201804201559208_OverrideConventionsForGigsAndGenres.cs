namespace LiveMusicLovers.Web.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverrideConventionsForGigsAndGenres : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gigs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Gigs", "Genro_Id", "dbo.Genroes");
            DropIndex("dbo.Gigs", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Gigs", new[] { "Genro_Id" });
            AlterColumn("dbo.Genroes", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Gigs", "Venue", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Gigs", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Gigs", "Genro_Id", c => c.Byte(nullable: false));
            CreateIndex("dbo.Gigs", "ApplicationUser_Id");
            CreateIndex("dbo.Gigs", "Genro_Id");
            AddForeignKey("dbo.Gigs", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Gigs", "Genro_Id", "dbo.Genroes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gigs", "Genro_Id", "dbo.Genroes");
            DropForeignKey("dbo.Gigs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Gigs", new[] { "Genro_Id" });
            DropIndex("dbo.Gigs", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Gigs", "Genro_Id", c => c.Byte());
            AlterColumn("dbo.Gigs", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Gigs", "Venue", c => c.String());
            AlterColumn("dbo.Genroes", "Name", c => c.String());
            CreateIndex("dbo.Gigs", "Genro_Id");
            CreateIndex("dbo.Gigs", "ApplicationUser_Id");
            AddForeignKey("dbo.Gigs", "Genro_Id", "dbo.Genroes", "Id");
            AddForeignKey("dbo.Gigs", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
