document.addEventListener("click", e => {
    let followPopup = document.querySelector(".follow_popup");
    if(!imageShowing && !followPopup.contains(e.target)
        && !document.querySelector(".total_followers").contains(e.target)
        && !document.querySelector(".total_followings").contains(e.target)) {
        closeFollowPopup();
    }
});