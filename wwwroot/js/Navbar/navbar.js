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
