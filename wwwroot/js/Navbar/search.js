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
/************************************************************************************************/
let searchInput = document.querySelector(".search_input");
let resultList = document.querySelector(".result_list");
let resultSearch = document.querySelector(".result_search");
let recentSearch = document.querySelector(".recent_search");
searchInput.addEventListener("input", function () {
    console.log(searchInput.value);
    resultList.textContent = '';
    resultSearch.classList.remove("show");
    resultSearch.classList.add("hide");
    recentSearch.classList.remove("show");
    recentSearch.classList.add("hide");
    if(searchInput.value.length > 2) {
        let matchList = [];
        accountList.forEach(account => {
           if(account['username'].includes(searchInput.value)) {
               matchList.push(account);
           } 
        });
        if(matchList.length !== 0) {
            resultSearch.classList.remove("hide");
            resultSearch.classList.add("show");
            matchList.forEach(account => {
                $('.result_list').append("<a class='recent_item' href='Profile?username="+account['username']+"'>" +
                    "<img class='avatar' src='"+account['avatarPath']+"' alt=''/>\n" +
                    "<label class='name'>"+account['displayedName']+"</label>\n" +
                    "</a>");
            });
        }
        else {
            $('.result_list').append("<p>Không tồn tại kết quả cho '"+searchInput.value+"'</p>")
        }
    }
    // else if(searchInput.value.length > 0) {
    //     $('.result_list').append("<p>Không tồn tại kết quả cho '"+searchInput.value+"'</p>")
    // }
    else if(searchInput.value.length === 0) {
        recentSearch.classList.remove("hide");
        recentSearch.classList.add("show");
    }
});
