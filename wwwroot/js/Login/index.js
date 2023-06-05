let g_interval;
let myFullpage = new fullpage('#fullpage', {
    licenseKey: 'OPEN-SOURCE-GPLV3-LICENSE',
    controlArrows: false,
    sectionsColor: ['#f2f2f2', 
                    '#4BBFC3',
                    'whitesmoke'],
});

const listTitle = document.querySelectorAll(".title_tail");
let currentPicIndex = 0;
setInterval(() => {
    listTitle[currentPicIndex].classList.add("hide");
    listTitle[currentPicIndex].classList.remove("show");
    if(currentPicIndex === listTitle.length -1) {
        currentPicIndex = 0;
    }
    else {
        currentPicIndex ++;
    }
    listTitle[currentPicIndex].classList.add("show");
    listTitle[currentPicIndex].classList.remove("hide");
}, 3000)

const images = [
    //food
    "https://i.pinimg.com/236x/e3/41/4b/e3414b2fcf00375a199ba6964be551af.jpg",
    "https://i.pinimg.com/236x/05/65/20/05652045e57af33599557db9f23188c0.jpg",
    "https://i.pinimg.com/236x/c5/83/53/c58353e15f32f3cbfc7cdcbcf0dc2f34--mango-coulis-m-sorry.jpg",
    "https://i.pinimg.com/564x/94/43/b9/9443b93bd8773fec91bc1837e8424e8e.jpg",
    "https://i.pinimg.com/564x/e6/8a/42/e68a42c2e530fbdf6b3ab2f379dcd384.jpg",
]

let imageList = document.querySelectorAll(".column_image");
imageList.forEach(image => {
    if (image.classList.contains("image1") || image.classList.contains("image6")) {
        image.src = images[0];
    }
    if (image.classList.contains("image2") || image.classList.contains("image7")) {
        image.src = images[1];
    }
    if (image.classList.contains("image3") || image.classList.contains("image8")) {
        image.src = images[2];
    }
    if (image.classList.contains("image4") || image.classList.contains("image9")) {
        image.src = images[3];
    }
    if (image.classList.contains("image5") || image.classList.contains("image0")) {
        image.src = images[4];
    }
});

let currentImageIndex = 0;
setInterval(() => {
    let isImage1Showed = document.querySelector(".image1").classList.contains("show");
    imageList.forEach(image => {
        if(image.classList.contains("show")) {
            image.classList.add("hide");
            image.classList.remove("show");
        }
    });
    
    if(isImage1Showed) {
        imageList.forEach(image => {
            if(image.classList.contains("image6")
                || image.classList.contains("image7") || image.classList.contains("image8")
                || image.classList.contains("image9") || image.classList.contains("image0")) {
                image.classList.add("show");
                image.classList.remove("hide");
            }
        });
    }
    else {
        imageList.forEach(image => {
            if(image.classList.contains("image1")
                || image.classList.contains("image2") || image.classList.contains("image3")
                || image.classList.contains("image4") || image.classList.contains("image5")) {
                image.classList.add("show");
                image.classList.remove("hide");
            }
        });
    }
}, 3000);

let toSignup = document.querySelector(".to_signup")
toSignup.addEventListener("click", () => {
    let loginBox = document.querySelector(".login-box")
    let signupBox = document.querySelector(".signup-box")
    loginBox.classList.remove("show_panel")
    loginBox.classList.add("hide_panel")
    
    signupBox.classList.remove("hide_panel")
    signupBox.classList.add("show_panel")
})

let toLogin = document.querySelector(".to_login")
toLogin.addEventListener("click", () => {
    let loginBox = document.querySelector(".login-box")
    let signupBox = document.querySelector(".signup-box")
    loginBox.classList.remove("hide_panel")
    loginBox.classList.add("show_panel")

    signupBox.classList.remove("show_panel")
    signupBox.classList.add("hide_panel")
})

// let sectionList = document.querySelectorAll(".section")
// document.addEventListener("scroll", e => {
//     for(let i = 0; i < sectionList.length; i++) {
//         if(sectionList[i].classList.contains("active")) {
//             if(i === 2) {
//                 navbar.classList.add("hide");
//                 navbar.classList.remove("show");
//             }
//             else {
//                 navbar.classList.add("show");
//                 navbar.classList.remove("hide");
//             }
//             break;
//         }
//     }  
// })

