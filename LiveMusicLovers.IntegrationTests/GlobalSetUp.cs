using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Persistence;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace LiveMusicLovers.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [SetUp]
        public void SetUp()
        {
            MigrateDbToLastestVesion();
            Seed();
        }

        private static void MigrateDbToLastestVesion()
        {
            var configuration = new Web.UI.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any())
                return;

                context.Users.Add(new ApplicationUser{UserName = "User1",Name = "User", PasswordHash = "-",Email = "-"});
            context.Users.Add(new ApplicationUser{UserName = "User2",Name = "User", PasswordHash = "-",Email = "-"});
            context.SaveChanges();
        }
    }
}
