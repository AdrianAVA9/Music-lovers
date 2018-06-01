var GigsDetailsController = function(relationshipServices) {

	var button;

	var fail = function() {
		console.log("Upps! Something failed");
	};

	var done = function() {
		button.text((button.text() === "Follow") ? "Following" : "Follow");
	};

	var toggleRelationship = function (e) {
		button = $(e.target);
		var artistId = button.attr("data-artist-id");

		if (button.text() === "Follow")
			relationshipServices.createRelationship(artistId,done,fail);
		else
			relationshipServices.unFollow(artistId,done,fail);
	};

	var init = function (container) {
		$(container).on("click",".js-toggle-relationship",toggleRelationship);
	};

	return {
		init: init
	};

}(RelationshipServices);