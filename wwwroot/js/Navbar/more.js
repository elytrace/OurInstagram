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

document.addEventListener("click", e => {
   if(sectionMore.classList.contains("show") && !sectionMore.contains(e.target) && !btnMore.contains(e.target)) {
       sectionMore.classList.remove("show");
       sectionMore.classList.add("hide");
   }
});