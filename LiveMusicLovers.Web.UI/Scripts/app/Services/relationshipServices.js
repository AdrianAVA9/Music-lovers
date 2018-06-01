var RelationshipServices = function() {
	var createRelationship = function (artistId,done,fail) {
		$.post("/api/relationships", { FolloweeId: artistId })
			.done(done)
			.fail(fail);
	};

	var unFollow = function (artistId,done,fail) {
		$.ajax({
				url: "/api/relationships/" + artistId,
				method: "DELETE"
			})
			.done(done)
			.fail(fail);
	};

	return {
		createRelationship : createRelationship,
		unFollow: unFollow
	};
}();