namespace LiveMusicLovers.Web.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGigTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genroes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Gigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Venue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Genro_Id = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Genroes", t => t.Genro_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Genro_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gigs", "Genro_Id", "dbo.Genroes");
            DropForeignKey("dbo.Gigs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Gigs", new[] { "Genro_Id" });
            DropIndex("dbo.Gigs", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Gigs");
            DropTable("dbo.Genroes");
        }
    }
}
