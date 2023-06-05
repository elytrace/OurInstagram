likeIcon = document.querySelector(".image_function").querySelector(".like");
unlikeBtn = likeIcon.querySelector("#unlike_btn");
likeBtn = likeIcon.querySelector("#like_btn");

function likeImage(imageId) {
    if (unlikeBtn.classList.contains("show")) {
        unlikeBtn.classList.remove("show");
        unlikeBtn.classList.add("hide");
        likeBtn.classList.add("show");
        likeBtn.classList.remove("hide");
    }
    else {
        unlikeBtn.classList.remove("hide");
        unlikeBtn.classList.add("show");
        likeBtn.classList.add("hide");
        likeBtn.classList.remove("show");
    }
    $.ajax({
        url: "/Navbar/LikeImage",
        type: "POST",
        data: { "imageId": imageId },
        success: function (data) {
            let total_like = document.querySelector(".total_like");
            total_like.innerHTML = data === "0" ? data + " Like" : data + " Likes";
        },
        error: function(error)
        {
            alert(error);
        }
    });
}

function deleteImage(imageId) {
    triggerNotification('submit', 'The process will take some minutes...');
    $.ajax({
        url: "/NavBar/DeleteImage/",
        type: "POST",
        data: { "imageId" : imageId },
        success: function (data) {
            // if (data.Success) {
            window.location.reload();
            triggerNotification('done', 'Image has been deleted!');
            // }
        },
        error: function(error)
        {
            triggerNotification('error', error);
        }
    });
    closeImagePopup();
}