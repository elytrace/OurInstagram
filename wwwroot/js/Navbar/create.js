/************************************************************************************************/
let btnCreate = document.querySelector(".create");
let createPanel = document.querySelector(".create_panel");
let createPopup = document.querySelector(".create_popup");
let editPopup = document.querySelector(".edit_popup");
let navbar = document.querySelector(".navbar");
let main = document.querySelector("main");

btnCreate.addEventListener("click", () => {
    navbar.classList.add("blur");
    main.classList.add("blur");
    createPanel.classList.add("show_popup");
});

/************************************************************************************************/
let imageInput = document.querySelector(".input_img");
let panelChoose = document.querySelector(".create_content");
let panelDisplay = document.querySelector(".display_section");
let uploaded_image;
imageInput.addEventListener("change", function() {
    const file_reader = new FileReader();
    file_reader.addEventListener("load", () => {
        uploaded_image = file_reader.result;
        document.querySelector(".display_image").style.backgroundImage = `url(${uploaded_image})`;

        panelChoose.classList.add("hide_upload");
        panelChoose.classList.remove("show_upload_flex");

        panelDisplay.classList.add("show_upload_flex");
        panelDisplay.classList.remove("hide_upload");
    });
    file_reader.readAsDataURL(this.files[0]);
});

/************************************************************************************************/
function uploadImage(url) {
    triggerNotification('submit', 'The process will take some minutes...');
    $.ajax({
        url: "/Home/UploadImage",
        type: "POST",
        data: { "imageURL" : url },
        success: function (data) {
            triggerNotification('done', 'Image has been uploaded!');
        },
        error: function(error)
        {
            triggerNotification('error', error);
        }
    });
    closeCreatePopup();
}

/************************************************************************************************/
function closeCreatePopup() {
    navbar.classList.remove("blur");
    main.classList.remove("blur");
    createPanel.classList.remove("show_popup");

    imageInput.value = null;
    document.querySelector(".display_image").style.backgroundImage = "unset";

    panelChoose.classList.add("show_upload_flex");
    panelChoose.classList.remove("hide_upload");

    panelDisplay.classList.add("hide_upload");
    panelDisplay.classList.remove("show_upload_flex");
    
    createPopup.classList.add("show_upload_flex");
    createPopup.classList.remove("hide_upload");

    editPopup.classList.add("hide_upload");
    editPopup.classList.remove("show_upload_block");
}

/************************************************************************************************/
function toEditPanel() {
    document.querySelector(".preview-img").querySelector("img").src = uploaded_image;

    createPopup.classList.add("hide_upload");
    createPopup.classList.remove("show_upload_flex");
    
    editPopup.classList.add("show_upload_block");
    editPopup.classList.remove("hide_upload");
}

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