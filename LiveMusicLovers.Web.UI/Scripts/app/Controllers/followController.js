var FollowController = function (relationshipServices) {
	var button;

	var done = function () {
		button.parents("li").fadeOut(function () {
			$(this).remove();
		});
	};

	var fail = function () {
		console.log("Upps! something fail");
	};

	var toggleFollow = function (e) {
		button = $(e.target);
		var artistId = button.attr("data-artist-id");

		relationshipServices.unFollow(artistId,done,fail);
	};

	var init = function (container) {
		$(container).on("click", ".js-toggle-follow", toggleFollow)
	};

	return {
		init:init
	}
}(RelationshipServices)