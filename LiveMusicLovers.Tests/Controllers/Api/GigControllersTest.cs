using FluentAssertions;
using LiveMusicLovers.Tests.Extensions;
using LiveMusicLovers.Web.UI.Controllers.Api;
using LiveMusicLovers.Web.UI.Core;
using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace LiveMusicLovers.Tests.Controllers.Api
{
    [TestClass]
    public class GigControllersTest
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IGigRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(g => g.Gigs).Returns(_mockRepository.Object);

            _controller = new GigsController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId,"adrian10596@live.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(15);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldBeReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(r => r.GetGigWithAttendace(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUserGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig {ArtistId = _userId + "-"};

            _mockRepository.Setup(r => r.GetGigWithAttendace(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturOk()
        {
            var gig = new Gig{ArtistId = _userId };

            _mockRepository.Setup(r => r.GetGigWithAttendace(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<OkResult>();
        }
    }
}
