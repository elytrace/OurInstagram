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

// HIDE POPUP WHEN CLICKING OUTSIDE
// document.addEventListener("click", (e) => {
//    let isClosest = e.target.closest(".more_menu");
//    if(sectionMore.classList.contains("hide")) return;
//    console.log(!isClosest + " " + sectionMore.classList.contains("show"));
//    if(!isClosest && sectionMore.classList.contains("show")) {
//        sectionMore.classList.remove("show");
//        sectionMore.classList.add("hide");
//    }
// });