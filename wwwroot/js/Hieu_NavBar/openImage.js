window.addEventListener("DOMContentLoaded", function () {
    let postList = document.querySelectorAll(".post");
    console.log(postList.length);
    postList.forEach(post => {
        post.addEventListener("click", function () {
            let navbar = document.querySelector(".navbar");
            let main = document.querySelector("main");
            let imageDetailPanel = document.querySelector(".image_details_panel");
            navbar.classList.add("blur");
            console.log(navbar.classList);
            main.classList.add("blur");
            imageDetailPanel.classList.add("show_popup");

            let image = post.querySelector("img");

        });

    });
});