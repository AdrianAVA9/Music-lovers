using FluentAssertions;
using LiveMusicLovers.Tests.Extensions;
using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Persistence;
using LiveMusicLovers.Web.UI.Persistence.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace LiveMusicLovers.Tests.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTest
    {
        private GigRepository _repository;
        private string _userId;
        private Mock<DbSet<Gig>> _mockGig;
        private Mock<DbSet<Attendance>> _mockAttendance;

        [TestInitialize]
        public void TestInitialize()
        {
            _userId = "1";
            _mockGig = new Mock<DbSet<Gig>>();
            _mockAttendance = new Mock<DbSet<Attendance>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(a => a.Gigs).Returns(_mockGig.Object);
            mockContext.SetupGet(a => a.Attendances).Returns(_mockAttendance.Object);

            _repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigsIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig(){DateTime = DateTime.Now.AddDays(-1),ArtistId = _userId};

             _mockGig.SetSource(new []{ gig });

            var gigs = _repository.GetUpcomingGigsByArtist(_userId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigsAreCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig {ArtistId = _userId};
            gig.Cancel();

            _mockGig.SetSource(new[]{gig});

            var gigs = _repository.GetUpcomingGigsByArtist(_userId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtists_GigsAreOk_ShouldBeReturned()
        {
            var gig = new Gig {DateTime = DateTime.Now.AddDays(6), ArtistId = _userId};

            _mockGig.SetSource(new[]{gig});

            var gigs = _repository.GetUpcomingGigsByArtist(_userId);

            gigs.Should().Contain(gig);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigsAreForADiferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig {ArtistId = _userId, DateTime = DateTime.Now.AddDays(3)};

            _mockGig.SetSource(new[]{gig});

            var gigs = _repository.GetUpcomingGigsByArtist(_userId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_ThereIsNotAnyAttendance_ShoulNotBeReturned()
        {
            var attendance = new Attendance
            {
                AttendeeId = _userId + "-",
                Gig = new Gig()
                {
                    ArtistId = "2",
                }
            };

            _mockAttendance.SetSource(new []{attendance});

            var gigs = _repository.GetGigsUserAttending(_userId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_ThereIsAttendance_ShoulBeReturned()
        {
            var attendance = new Attendance
            {
                AttendeeId = _userId,
                Gig = new Gig()
                {
                    ArtistId = "2",
                    DateTime = DateTime.Now.AddDays(2),
                }
            };

            _mockAttendance.SetSource(new []{attendance});

            var gigs = _repository.GetGigsUserAttending(_userId);

            gigs.Should().Contain(attendance.Gig);
        }

        [TestMethod]
        public void GetGigsUserAttending_GigsAreInThePast_ShouldNotBeReturned()
        {
            var attendance = new Attendance
            {
                AttendeeId = _userId,
                Gig = new Gig { DateTime = DateTime.Now.AddDays(-1)}
            };

            _mockAttendance.SetSource(new[]{attendance});

            var gigs = _repository.GetGigsUserAttending(_userId);
            gigs.Should().BeEmpty();
        }
    }
}
