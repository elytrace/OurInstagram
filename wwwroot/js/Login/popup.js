let btnLogin = document.querySelector(".btn_login");
let btnSignup = document.querySelector(".btn_signup");

let panelLogin = document.querySelector(".panel_login");
let panelSignup = document.querySelector(".panel_signup");

let loginPopup = document.querySelector(".login_popup");
let signupPopup = document.querySelector(".signup_popup");

let navbar = document.querySelector(".navbar");
let main = document.querySelector(".main");

btnLogin.addEventListener("click", () => {
    navbar.classList.add("blur");
    main.classList.add("blur");
    panelLogin.classList.add("show_popup");
});
btnSignup.addEventListener("click", () => {
    navbar.classList.add("blur");
    main.classList.add("blur");
    panelSignup.classList.add("show_popup");
});

function closePopup() {
    navbar.classList.remove("blur");
    main.classList.remove("blur");
    panelLogin.classList.remove("show_popup");
    panelSignup.classList.remove("show_popup");
}

window.addEventListener("DOMContentLoaded", () => {
    if(panelSignup.classList.contains("show_popup")) {
        navbar.classList.add("blur");
        main.classList.add("blur");
    }
    if(panelLogin.classList.contains("show_popup")) {
        navbar.classList.add("blur");
        main.classList.add("blur");
    }
})