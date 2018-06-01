using FluentAssertions;
using LiveMusicLovers.Tests.Extensions;
using LiveMusicLovers.Web.UI.Controllers.Api;
using LiveMusicLovers.Web.UI.Core;
using LiveMusicLovers.Web.UI.Core.Dto;
using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace LiveMusicLovers.Tests.Controllers.Api
{
    [TestClass]
    public class AttendingControllerTest
    {
        private Mock<IAttendanceRepository> _mockRepository;
        private AttendancesController _controller;
        private string _userId;

        [TestInitialize]
        public void TestInicialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.SetupGet(a => a.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUnitOfWork.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "adrian10596@live.com");
        }

        [TestMethod]
        public void CancelAttend_ValidRequest_ShouldReturnOk()
        {
            var attendance = new Attendance();

            _mockRepository.Setup(a => a.IsGoing(1,_userId)).Returns(attendance);

            var result = _controller.CancelAttendance(1);

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void CancelAttend_NotGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.CancelAttendance(15);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void CancelAttend_GigIsCanceled_ShouldReturnBadRequest()
        {
            var attendance = new Attendance
            {
                Gig = new Gig(),
            };

            attendance.Gig.Cancel();

            _mockRepository.Setup(g => g.IsGoing(1, _userId)).Returns(attendance);

            var result = _controller.CancelAttendance(1);

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_UserAlreadyAttendanceToThisGig_ShouldReturnBadRequestErrorMessage()
        {
            var attendance = new Attendance
            {
                GigId = 1,
                AttendeeId = _userId,
            };

            _mockRepository.Setup(a => a.IsGoing(1, _userId)).Returns(attendance);

            var result = _controller.Attend(new AttendanceDto{GigId = 1});

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_NotExistentAttendace_ShouldReturnOk()
        {
            var result = _controller.Attend(new AttendanceDto {GigId = 15});

            result.Should().BeOfType<OkResult>();
        }
    }
}
