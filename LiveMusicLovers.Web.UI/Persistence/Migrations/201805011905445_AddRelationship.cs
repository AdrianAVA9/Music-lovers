namespace LiveMusicLovers.Web.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Relationships",
                c => new
                    {
                        Followeeid = c.String(nullable: false, maxLength: 128),
                        FollowerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Followeeid, t.FollowerId })
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId)
                .ForeignKey("dbo.AspNetUsers", t => t.Followeeid)
                .Index(t => t.Followeeid)
                .Index(t => t.FollowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relationships", "Followeeid", "dbo.AspNetUsers");
            DropForeignKey("dbo.Relationships", "FollowerId", "dbo.AspNetUsers");
            DropIndex("dbo.Relationships", new[] { "FollowerId" });
            DropIndex("dbo.Relationships", new[] { "Followeeid" });
            DropTable("dbo.Relationships");
        }
    }
}
