

function closeImagePopup() {
    imageShowing = false;
    navbar.classList.remove("blur");
    main.classList.remove("blur");
    imageDetailPanel.classList.remove("show_popup");
}

document.addEventListener("click", e => {
    if (!createPopup.contains(e.target) && !btnCreate.contains(e.target) && createPanel.classList.contains("show_popup")) {
        closeCreatePopup();
    }
});

/************************************************************************************************/
let btnClose = document.querySelector(".close_img");
btnClose.addEventListener("click", () => closeCreatePopup());
let btnCancel = document.querySelector(".btn_cancel");
btnCancel.addEventListener("click", () => closeCreatePopup());

function triggerNotification(type, message) {
    console.log((type === 'submit') + " " + message + " " + jQuery('#notification').length);
    if(jQuery('#notification').length) 
        jQuery('#notification').remove();
    
    jQuery(document.body).append(
        "<div class='push-notification' id='notification'>"+
            "<div class='push_icon push-"+type+"'></div>" +
            "<p class='push_message'>"+message+"</p>" +
        "</div>"
    );
    if(type === 'submit') {
        jQuery('#notification').show();
    }
    else {
        jQuery('#notification').show()
            .fadeOut(5000, function () {
            jQuery('#notification').remove();
        });
    }
}