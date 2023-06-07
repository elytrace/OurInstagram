const filterOptions = document.querySelectorAll(".filter button"),
    filterName = document.querySelector(".filter-info .name"),
    filterValue = document.querySelector(".filter-info .value"),
    filterSlider = document.querySelector(".slider input"),
    rotateOptions = document.querySelectorAll(".rotate button"),
    previewImg = document.querySelector(".preview-img img"),
    resetFilterBtn = document.querySelector(".reset-filter");

let brightness = "100", saturation = "100", inversion = "0", grayscale = "0";
let rotate = 0, flipHorizontal = 1, flipVertical = 1;

const applyFilter = () => {
    previewImg.style.transform = `rotate(${rotate}deg) scale(${flipHorizontal}, ${flipVertical})`;
    previewImg.style.filter = `brightness(${brightness}%) saturate(${saturation}%) invert(${inversion}%) grayscale(${grayscale}%)`;
}

filterOptions.forEach(option => {
    option.addEventListener("click", () => {
        document.querySelector(".active").classList.remove("active");
        option.classList.add("active");
        filterName.innerText = option.innerText;

        if(option.id === "brightness") {
            filterSlider.max = "200";
            filterSlider.value = brightness;
            filterValue.innerText = `${brightness}%`;
        } else if(option.id === "saturation") {
            filterSlider.max = "200";
            filterSlider.value = saturation;
            filterValue.innerText = `${saturation}%`
        } else if(option.id === "inversion") {
            filterSlider.max = "100";
            filterSlider.value = inversion;
            filterValue.innerText = `${inversion}%`;
        } else {
            filterSlider.max = "100";
            filterSlider.value = grayscale;
            filterValue.innerText = `${grayscale}%`;
        }
    });
});

const updateFilter = () => {
    filterValue.innerText = `${filterSlider.value}%`;
    const selectedFilter = document.querySelector(".filter .active");

    if(selectedFilter.id === "brightness") {
        brightness = filterSlider.value;
    } else if(selectedFilter.id === "saturation") {
        saturation = filterSlider.value;
    } else if(selectedFilter.id === "inversion") {
        inversion = filterSlider.value;
    } else {
        grayscale = filterSlider.value;
    }
    applyFilter();
}

rotateOptions.forEach(option => {
    option.addEventListener("click", () => {
        if(option.id === "left") {
            rotate -= 90;
        } else if(option.id === "right") {
            rotate += 90;
        } else if(option.id === "horizontal") {
            flipHorizontal = flipHorizontal === 1 ? -1 : 1;
        } else {
            flipVertical = flipVertical === 1 ? -1 : 1;
        }
        applyFilter();
    });
});

const resetFilter = () => {
    brightness = "100"; saturation = "100"; inversion = "0"; grayscale = "0";
    rotate = 0; flipHorizontal = 1; flipVertical = 1;
    filterOptions[0].click();
    applyFilter();
}

filterSlider.addEventListener("input", updateFilter);
resetFilterBtn.addEventListener("click", resetFilter);

let editPanel = document.querySelector(".edit_panel");
let currentImage;
function openEditPanel(imageId, imageSrc) {
    currentImage = imageId;
    navbar.classList.add("blur");
    main.classList.add("blur");
    editPanel.classList.add("show_popup");
    let imageDetailPanel = document.querySelector(".image_details_panel");
    imageDetailPanel.classList.remove("show_popup");

    if(imageId !== -1) {
        previewImg.setAttribute("src", imageSrc); 
    }
    else {
        previewImg.classList.add("hide");
    }
    // console.log(editPanel.querySelector(".preview-img").querySelector("img").src);   
}

function saveEditImage() {
    const canvas = document.createElement("canvas");
    const ctx = canvas.getContext("2d");
    canvas.width = previewImg.naturalWidth;
    canvas.height = previewImg.naturalHeight;

    ctx.filter = `brightness(${brightness}%) saturate(${saturation}%) invert(${inversion}%) grayscale(${grayscale}%)`;
    ctx.translate(canvas.width / 2, canvas.height / 2);
    if(rotate !== 0) {
        ctx.rotate(rotate * Math.PI / 180);
    }
    ctx.scale(flipHorizontal, flipVertical);
    ctx.drawImage(previewImg, -canvas.width / 2, -canvas.height / 2, canvas.width, canvas.height);
    
    triggerNotification('submit', 'The process will take some minutes...');
    $.ajax({
        url: "/Profile/ConfirmEditImage/",
        type: "POST",
        data: { "imageId" : currentImage, "imageURL" : canvas.toDataURL() },
        success: function (data) {
            if(currentImage === -1) {
                document.querySelector(".avatar_section .avatar").src = data;
            }
            triggerNotification('done', 'Image has been uploaded!');
        },
        error: function(error)
        {
            triggerNotification('error', error);
        }
    });
    resetFilter();
    closeEditPanel();
}

function closeEditPanel() {
    navbar.classList.remove("blur");
    main.classList.remove("blur");
    editPanel.classList.remove("show_popup");
}

let btnUpload = document.querySelector(".preview-img").querySelectorAll("label, input");
$("#file").change(function() {
    const file_reader = new FileReader();
    file_reader.addEventListener("load", () => {
        previewImg.classList.remove("hide");
        previewImg.src = file_reader.result;
        btnUpload.forEach(ele => ele.classList.add("hide"));
    });
    file_reader.readAsDataURL(this.files[0]);
});
