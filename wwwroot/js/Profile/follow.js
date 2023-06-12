function follow(userId) {
    changeFollowState();
    $.ajax({
        url: "/Profile/Follow",
        type: "POST",
        data: { "userId": userId },
        success: function (data) {
            let total_followers = document.querySelector(".total_followers");
            total_followers.innerHTML = '<strong>' + data + '</strong>' + " Follower" + (data === 0 ? "" : "s");
        },
        error: function(error)
        {
            triggerNotification('error', error);
        }
    });
}
