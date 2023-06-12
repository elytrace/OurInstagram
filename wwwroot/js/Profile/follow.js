function follow(userId) {
    let labelFollow = document.querySelector(".label_follow");
    let labelUnfollow = document.querySelector(".label_unfollow");
    changeFollowState(labelFollow, labelUnfollow);
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

function followOnly(button, userId) {
    let parent = button.parentNode;
    let labelFollow = parent.querySelector(".label_follow");
    let labelUnfollow = parent.querySelector(".label_unfollow");
    changeFollowState(labelFollow, labelUnfollow);
    $.ajax({
        url: "/Profile/FollowDirectly/",
        type: "POST",
        data: { "userId": userId },
        success: function (data) {
            let total_followings = document.querySelector(".total_followings");
            total_followings.innerHTML = '<strong>' + data + '</strong>' + " Follower" + (data === 0 ? "" : "s");
        },
        error: function(error)
        {
            triggerNotification('error', error);
        }
    });
}
