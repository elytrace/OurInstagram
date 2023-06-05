/************************************************************************************************/
let btnMore = document.querySelector(".section_more");
let sectionMore = document.querySelector(".more_menu");

// DISPLAY 'MORE' POPUP ON CLICK
btnMore.addEventListener("click", () => {
    if(sectionMore.classList.contains("hide")) {
        sectionMore.classList.add("show");
        sectionMore.classList.remove("hide");
    }
    else {
        sectionMore.classList.remove("show");
        sectionMore.classList.add("hide");
    }
});
