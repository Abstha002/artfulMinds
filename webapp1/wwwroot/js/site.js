// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Navbar hover animation
document.querySelectorAll(".nav-links a").forEach((link) => {
    link.addEventListener("mouseover", () => {
        link.style.color = "#3AB0FF";
    });
    link.addEventListener("mouseout", () => {
        link.style.color = "";
    });
});

// Smooth scrolling for internal links
document.querySelectorAll('a[href^="#"]').forEach((anchor) => {
    anchor.addEventListener("click", function (e) {
        e.preventDefault();
        document.querySelector(this.getAttribute("href")).scrollIntoView({
            behavior: "smooth",
        });
    });
});


// Wait for the DOM content to be fully loaded
document.addEventListener("DOMContentLoaded", function () {
    const postDescription = document.querySelector('.post-description');
    const readMoreButton = document.querySelector('.read-more');
    const postCard = document.querySelector('.post-card');
    const postDescriptionWrapper = document.querySelector('.post-description-wrapper');

    // Detect when the typing animation ends and show the "Read More" button
    postDescription.addEventListener('animationend', function () {
        postDescription.classList.add('finished'); // Mark typing as finished
        // After typing animation ends, show the "Read More" button
        setTimeout(() => {
            readMoreButton.style.display = 'inline-block';
        }, 1000); // Add a delay to ensure the animation completes
    });

    // Add event listener for the "Read More" button
    readMoreButton.addEventListener('click', function () {
        postCard.classList.toggle('expanded');
        // Expand the height of the description when "Read More" is clicked
        if (postCard.classList.contains('expanded')) {
            postDescriptionWrapper.style.height = 'auto'; // Remove the fixed height to show full content
        } else {
            postDescriptionWrapper.style.height = '120px'; // Reapply the height limit if collapsed
        }
    });
});