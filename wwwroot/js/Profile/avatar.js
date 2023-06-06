let avatar = document.querySelector(".avatar_overlay");
let avatarInput = document.querySelector(".avatar_section").querySelector("input");

function changeAvatar() {
    avatarInput.addEventListener("change", function() {
        const file_reader = new FileReader();
        file_reader.addEventListener("load", () => {
            uploaded_image = file_reader.result;
            console.log(uploaded_image);
            document.querySelector(".avatar").src = uploaded_image;
        });
        file_reader.readAsDataURL(this.files[0]);
    });
}

avatar.addEventListener("click", changeAvatar);