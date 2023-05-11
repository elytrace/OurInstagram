let btnSearch = document.querySelector(".search");
btnSearch.addEventListener("click", () => {
    let searchPane = document.querySelector(".search_panel");
    let notificationPane  = document.querySelector(".notify_panel");
    if(searchPane.classList.contains("hide")) {
        searchPane.classList.add("show");
        searchPane.classList.remove("hide");
        if(notificationPane.classList.contains("show")){
            notificationPane.classList.remove("show");
            notificationPane.classList.add("hide");
        }
    }
    else {
        searchPane.classList.remove("show");
        searchPane.classList.add("hide");
    }
})