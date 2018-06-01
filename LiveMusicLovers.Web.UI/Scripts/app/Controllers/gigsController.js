var GigsController = function (attendanceService) {
	var button;

	var done = function () {
		console.log(button.text());
		var text = (button.text() === "Going") ? "Going?" : "Going";

		button.toggleClass("btn-info").toggleClass("btn-default").text(text);
	};

	var fail = function (message) {
		console.log("Upps!! something fail");
	};

	var toggleAttendance = function (e) {

		button = $(e.target);
		var gigId = button.attr("data-gig-id");

		if (button.hasClass("btn-default"))
			attendanceService.createAttendance(gigId, done, fail);
		else
			attendanceService.cancelAttendance(gigId, done, fail);
	};

	var init = function (container) {
		$(".js-toggle-attendances").click(toggleAttendance);

	};

	return {
		init: init
	};
}(AttendancesService);