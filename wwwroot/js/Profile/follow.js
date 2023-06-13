function follow(userId) {
    let labelFollow = document.querySelector(".label_follow");
    let labelUnfollow = document.querySelector(".label_unfollow");
    let isConfirmed = changeFollowState(labelFollow, labelUnfollow);
    if(!isConfirmed) return;
    $.ajax({
        url: "/Profile/Follow",
        type: "POST",
        data: { "userId": userId },
        success: function (data) {
            let total_followers = document.querySelector(".total_followers");
            total_followers.innerHTML = '<strong>' + data.cnt + '</strong>' + " Follower" + (data === 0 ? "" : "s");
        },
        error: function(error)
        {
            triggerNotification('error', error);
        }
    });
}

function followOnly(button, userId, panel) {
    let parent = button.parentNode;
    let labelFollow = parent.querySelector(".label_follow");
    let labelUnfollow = parent.querySelector(".label_unfollow");
    // console.log("followed");
    let isConfirmed = changeFollowState(labelFollow, labelUnfollow);
    if(!isConfirmed) return;
    $.ajax({
        url: "/Profile/FollowDirectly/",
        type: "POST",
        data: { "userId": userId },
        success: function (data) {
            let total_followings = document.querySelector(".total_followings");
            total_followings.innerHTML = '<strong>' + data.cnt + '</strong>' + " Following" + (data === 0 ? "" : "s");
            if(!data.following && panel === 'following') {
                $(".follow_list").remove(parent);
                let url = "Profile/FollowingPanel";
                $(".following_panel").load(url, { userId: 0 });
            }
        },
        error: function(error)
        {
            triggerNotification('error', error);
        }
    });
}