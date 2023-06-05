/************************************************************************************************/
let btnSearch = document.querySelector(".search");
let searchPane = document.querySelector(".search_panel");

const btnMap = new Map();
btnMap.set(btnSearch, searchPane);

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