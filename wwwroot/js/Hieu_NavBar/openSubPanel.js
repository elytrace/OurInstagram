/************************************************************************************************/
let btnSearch = document.querySelector(".search");
let btnMessage = document.querySelector(".message");
let btnNotification = document.querySelector(".notify");

let searchPane = document.querySelector(".search_panel");
let messagePane  = document.querySelector(".message_panel");
let notificationPane  = document.querySelector(".notify_panel");
    
const btnMap = new Map();
btnMap.set(btnSearch, searchPane);
btnMap.set(btnMessage, messagePane);
btnMap.set(btnNotification, notificationPane);

for(let [key, value] of btnMap) {
    key.addEventListener("click", () => {
        if(value.classList.contains("hide")) {
            for(let [_key, _value] of btnMap) {
                _value.classList.remove("show");
                _value.classList.add("hide");
            }
            value.classList.remove("hide");
            value.classList.add("show");
        }
        else {
            value.classList.remove("show");
            value.classList.add("hide");
        }
    });
}

/************************************************************************************************/
let btnCreate = document.querySelector(".create");
let createPanel = document.querySelector(".create_panel");
let createPopup = document.querySelector(".create_popup");
let navbar = document.querySelector(".navbar");
let main = document.querySelector("main");

btnCreate.addEventListener("click", () => {
    navbar.classList.add("blur");
    main.classList.add("blur");
    createPanel.classList.add("show_popup");
});

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

/************************************************************************************************/
document.addEventListener("click", e => {
    if (!createPopup.contains(e.target) && !btnCreate.contains(e.target)) {
        navbar.classList.remove("blur");
        main.classList.remove("blur");
        createPanel.classList.remove("show_popup");
    }
});

/************************************************************************************************/
let btnClose = document.querySelector(".close_img");
btnClose.addEventListener("click", () => {
    navbar.classList.remove("blur");
    main.classList.remove("blur");
    createPanel.classList.remove("show_popup");
});

/************************************************************************************************/
let blankImageMap = new Map();
let fillImageMap = new Map();
let funcNameList = ["Home", "Search", "Explore", "Message", "Notification", "Create", "Profile"];
funcNameList.forEach(name => {
    blankImageMap.set(name, name.toLowerCase() + "_blank");
    fillImageMap.set(name, name.toLowerCase() + "_fill");
});

blankImageMap.set("More", "burger");
fillImageMap.set("More", "burger");

let functions = document.querySelectorAll(".function")
functions.forEach(func => {
    func.addEventListener("click", () => {
        let funcImage = func.querySelector(".function_img");
        let funcName = func.querySelector(".function_name");
        functions.forEach(otherFunc => {
            let otherFuncImage = otherFunc.querySelector(".function_img");
            let otherFuncName = otherFunc.querySelector(".function_name");
            otherFuncImage.src = "Resources/" + blankImageMap.get(otherFuncName.id) + ".png";
            otherFuncName.classList.remove("current_function");
        })
        if(!funcName.classList.contains("current_function")) {
            funcImage.src = "Resources/" + fillImageMap.get(funcName.id) + ".png";
            funcName.classList.add("current_function");
        }
    });
});