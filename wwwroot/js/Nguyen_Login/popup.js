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

document.addEventListener("click", e => {
    if (!signupPopup.contains(e.target) && !loginPopup.contains(e.target) && !btnLogin.contains(e.target)  && !btnSignup.contains(e.target)) {
        navbar.classList.remove("blur");
        main.classList.remove("blur");
        panelLogin.classList.remove("show_popup");
        panelSignup.classList.remove("show_popup");
    }
});