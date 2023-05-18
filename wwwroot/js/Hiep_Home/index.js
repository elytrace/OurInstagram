// some js

// push items
const posts = []

const images = [
    'https://i.pinimg.com/564x/2d/c5/cc/2dc5cc5aae9480338f79147b5282d1a0.jpg',
    'https://i.pinimg.com/564x/43/fe/09/43fe095612b0349091f95ba2f8858160.jpg',
    'https://i.pinimg.com/736x/7f/2d/a9/7f2da9cdaaba31c68503277be1ee2d81.jpg',
    'https://i.pinimg.com/736x/6f/80/ef/6f80efd4b1b7fa69c65fe89da0c43bde.jpg',
    'https://i.pinimg.com/564x/19/a7/66/19a76673d8336da17b58e2b75cccfc07.jpg',
    'https://i.pinimg.com/564x/86/5b/7c/865b7c4169bd49e029823892440c1a3c.jpg',
    'https://i.pinimg.com/564x/12/28/cc/1228cc1710e1fb30bcc5c9728bc08754.jpg',
    'https://i.pinimg.com/736x/18/1d/5c/181d5cb3eb6a0ed8b846445e3ad6b6c1.jpg',
    'https://i.pinimg.com/564x/89/b1/3a/89b13a125283b43640c80d5de5d79023.jpg',
    'https://i.pinimg.com/736x/a1/bc/22/a1bc224d57870fcdc8e83eedfcc41814.jpg',
    'https://i.pinimg.com/564x/c5/b3/20/c5b3200717e6a1015283235df6a01f08.jpg',
    'https://i.pinimg.com/564x/52/4a/cb/524acb06a7f5c5789288a45a30b0b8f7.jpg',
    'https://i.pinimg.com/564x/e2/6e/79/e26e7921b2be1cf08aecc05655e10777.jpg',
    'https://i.pinimg.com/564x/e5/d2/ea/e5d2ea7ceaff183c7e840af1f23a2b76.jpg',
    'https://i.pinimg.com/736x/7a/ea/10/7aea100e3b35970ee1c5eefb23169547.jpg'
]

let imageIndex = 0;

for (let i = 1; i <= 80; i++) {
    let item = {
        id: i,
        image: images[imageIndex]
    }
    posts.push(item);
    imageIndex++;
    if (imageIndex > images.length - 1) imageIndex = 0;
}

// apps


const container = document.querySelector('.homepage_container');

function generateMasonryGrid(columns, posts) {

    container.innerHTML = '';

    let columnWrappers = {};

    for (let i = 0; i < columns; i++) {
        columnWrappers[`column${i}`] = [];
    }

    for (let i = 0; i < posts.length; i++) {
        const column = i % columns;
        columnWrappers[`column${column}`].push(posts[i]);
    }

    for (let i = 0; i < columns; i++) {
        let columnPosts = columnWrappers[`column${i}`];
        let div = document.createElement('div');
        div.classList.add('column');

        columnPosts.forEach(post => {
            let postDiv = document.createElement('div');
            postDiv.classList.add('post');
            let image = document.createElement('img');
            image.src = post.image;
            let hoverDiv = document.createElement('div');
            hoverDiv.classList.add('overlay');
            
            postDiv.append(image, hoverDiv)
            div.appendChild(postDiv)
        });
        container.appendChild(div);
    }
}

let previousScreenSize = window.innerWidth;


window.addEventListener("DOMContentLoaded", () => {
    imageIndex = 0;
    generateMasonryGrid(4, posts);
    previousScreenSize = window.innerWidth;
})

// Responsive
window.addEventListener('resize', () => {
    imageIndex = 0;
    if (window.innerWidth < 600 && previousScreenSize >= 600) {
        generateMasonryGrid(1, posts);
    } else if (window.innerWidth >= 600 && window.innerWidth < 1000 && (previousScreenSize < 600 || previousScreenSize >= 1000)) {
        generateMasonryGrid(2, posts);

    } else if (window.innerWidth >= 1000 && previousScreenSize < 1000) {
        generateMasonryGrid(4, posts)
    }
    previousScreenSize = window.innerWidth;
})

// if(previousScreenSize < 600){
//     generateMasonryGrid(1, posts)
// }else if(previousScreenSize >= 600 && previousScreenSize < 1000){
//     generateMasonryGrid(2, posts)
// }else{
//     generateMasonryGrid(4, posts)
// }
