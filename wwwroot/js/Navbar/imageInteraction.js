/************************************************************************************************/
let imageDetailPanel = document.querySelector(".image_details_panel");

window.addEventListener("DOMContentLoaded", function () {
    let postList = document.querySelectorAll(".post");
    console.log(postList.length);
    for(let i = 0; i < postList.length; i++) {
        let post = postList[i];

        post.addEventListener("click", function () {
            navbar.classList.add("blur");
            main.classList.add("blur");
            imageDetailPanel.classList.add("show_popup");
            console.log(postList[i].id);
            $(".image_details_panel").load(actionPath, { imageId: postList[i].id });
        });
    }
});

/************************************************************************************************/
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

/************************************************************************************************/
function selectEditDelete() {
    let menuEditDelete = document.querySelector(".edit_delete");

    if(menuEditDelete.classList.contains("show")) {
        menuEditDelete.classList.remove("show");
        menuEditDelete.classList.add("hide");
    }
    else {
        menuEditDelete.classList.remove("hide");
        menuEditDelete.classList.add("show");
    }
}