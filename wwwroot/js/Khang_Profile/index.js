// some js
function loadingImages(images) {
    const posts = []

    let imageIndex = 0;

    for (let i = 1; i <= images.length; i++) {
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
}
