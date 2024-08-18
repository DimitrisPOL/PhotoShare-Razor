var myVar = setInterval(myTimer, 3000);

document.onload = function (ev) {
    document.getElementById("counter").value = 0;
    plusSlides(1, null);
};

function myTimer() {
    plusSlides(1, null);
}

window.onload = function (ev) {
    document.getElementById("counter").value = 0;
    document.getElementById("index").value = 0;
    plusSlides(1, null);
};

var slideIndex = 1;
showSlides(slideIndex, null);

function plusSlides(n, id) {
    showSlides(slideIndex += n, id);
}

function currentSlide(n) {
    showSlides(slideIndex = n, null);
}

function showSlides(n, id) {
    var i;
    var slides;
    var dots;
    var texts;

    if (id == null) {
        slides = document.getElementsByClassName("mySlides");
        dots = document.getElementsByClassName("dot");
        texts = document.getElementsByClassName("text-slider");
    }
    else {
        slides = document.getElementById(id).getElementsByClassName("mySlides");
        dots = document.getElementById(id).getElementsByClassName("dot");
        texts = document.getElementById(id).getElementsByClassName("text-slider");
    }

    if (n > 3) { slideIndex = 1 }
    if (n < 1) { slideIndex = 3 }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    if (slides[slideIndex - 1])
        slides[slideIndex - 1].style.display = "block";
    if (slides[slideIndex + 2])
        slides[slideIndex + 2].style.display = "block";
}