using FluentAssertions;
using LiveMusicLovers.IntegrationTests.Extensions;
using LiveMusicLovers.Web.UI.Controllers;
using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Core.ViewModels;
using LiveMusicLovers.Web.UI.Persistence;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveMusicLovers.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigControllerTests
    {
        private GigController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test,Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            //Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id,user.UserName);

            var genre = _context.Genres.First();
            var gig = new Gig {Artist = user, DateTime = DateTime.Now.AddDays(3),Genre = genre, Venue = "San Jose"};
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act
            var result = _controller.Mine();

            //Assert
            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }

        [Test,Isolated]
        public void Update_WhenCalled_ShouldUpdatetheGiveGig()
        {
            //Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id,user.UserName);

            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig {Artist = user, DateTime = DateTime.Now.AddDays(3),Genre = genre, Venue = "San Jose"};
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            var date = new DateTime(2018,6,14);
            //Act
            var result = _controller.Update(new GigForViewModel
            {
                Date = date.ToString("d MMM yyyy"),
                Time = "20:00",
                Venue = "Venue",
                Genre = 2,
            });

            //Assert
            _context.Entry(gig).Reload();
            gig.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            gig.Venue.Should().Be("Venue");
            gig.Genre.Should().Be(2);
        }
    }
}
