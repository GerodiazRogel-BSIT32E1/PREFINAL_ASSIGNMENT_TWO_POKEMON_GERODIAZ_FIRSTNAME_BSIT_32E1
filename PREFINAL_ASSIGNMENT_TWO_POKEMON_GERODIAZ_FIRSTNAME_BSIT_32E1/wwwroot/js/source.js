const toggle = document.querySelector(".dark-mode-toggle img");
let originalSrc = toggle.getAttribute("src");
let alternateSrc = toggle.getAttribute("data-src");

// Set default mode to dark mode
document.body.classList.add("dark-mode");
toggle.src = alternateSrc;

toggle.addEventListener("click", () => {
  document.body.classList.toggle("dark-mode");
  if (document.body.classList.contains("dark-mode")) {
    toggle.src = alternateSrc;
  } else {
    toggle.src = originalSrc;
  }
});