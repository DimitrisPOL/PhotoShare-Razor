
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//const { main } = require("@popperjs/core");

window.fbAsyncInit = function () {
    FB.init({
        appId: '566054317747874',
        autoLogAppEvents: true,
        xfbml: true,
        version: 'v11.0'
    });
};

function myShare(s) {
    alert(s);
    FB.ui({
        method: 'share',
        href: "https://www.facebook.com/dialog/share?app_id=566054317747874&display=popup&href=http%3A%2F%2F166764593.winzone58.grserver.gr%2FMemory-Pic%2FPhotoDetails%3FphotoUrl%3Dhttps%253a%252f%252fstoragetesting2244.blob.core.windows.net%252foia%252fland7.jpg&redirect_uri=http%3A%2F%2F166764593.winzone58.grserver.gr%2Ftools%2Fexplorer",
    }, function (response) { });
};

// Write your Javascript code.
function toggleDropdown() {
    document.getElementById("myDropdown").classList.toggle("show");
}

// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

var myVar = setInterval(myTimer, 3000);

var count = 0;
document.onload = function (ev) {
    document.getElementById("counter").value = 0;
    plusSlides(1);


};


function myTimer() {
    plusSlides(1);
}

window.onload = function (ev) {
    document.getElementById("counter").value = 0;
    document.getElementById("index").value = 0;
    plusSlides(1);
};
var indexCounter = 0;
window.onscroll = function (ev) {
    //alert(document.documentElement.clientHeight + "  " + document.body.offsetHeight);
    if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight - 50) {

        //const sum = $(window).scrollTop() + $(window).height() + 20;
        //var indexCounter = document.getElementById("counter").value++;
        //  indexCounter = indexCounter + parseInt("1");

        //const conuter = Number(document.getElementById("counter").value);
        //alert(conuter);
        var search = document.getElementById("txtLocation").value;
        var images = document.getElementsByClassName("front-image");
        if (images != null) {

            /*          console.log(images.length + " and" + document.getElementById("counter").value);*/
            indexCounter = images.length;

            if (images.length == document.getElementById("counter").value) {
                return null;
            }

            document.getElementById("counter").value = images.length;
        }

        console.log(count + " --- " + document.getElementById("counter").value + " -- " + window.innerHeight + " -- " + window.scrollY + " -- " + document.body.offsetHeight);
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {


            if (this.readyState == 4 && this.status == 200) {
                //document.getElementById("demo").innerHTML =
                var arr_from_json = JSON.parse(this.responseText);
                arr_from_json.forEach(
                    function (item, index) {

                        var uri = encodeURIComponent(item);
                        //<div class=\"main-pic-menu\"><a class=\"share-ico\" href=\"\" ><img onclick =\"myShare(\""+document.location.protocol + "\\\\" + document.location.hostname + ":" + document.location.port +"\\" + "PhotoDetails?photoUrl="+ item + "\") src = \"~/images/facebook-share-button3.png\" /></a > <a class=\"download-ico\" href=\"" + item + "\" download> <img src=\"/images/download-icon.png\" /> </a></div > 
                        var html = "<a href=\"/Pictures/Details/" + uri + "\"><div class=\"main-pic\" ><img class=\"front-image\" src=\"" + item + "\" /></div></a>";
                        var x = document.createElement("div");

                        /*               x.setAttribute("src", item);*/
                        x.setAttribute("class", "main-pic-container");
                        x.innerHTML = html;
                        var main_pic = document.createElement("div");
                        /*               x.setAttribute("src", item);*/
                        main_pic.setAttribute("class", "main-pic-menu");
                        var a = document.createElement("a");
                        /*               x.setAttribute("src", item);*/
                        a.setAttribute("class", "share-ico");
                        a.setAttribute("href", "");

                        var img = document.createElement("img");
                        /*               x.setAttribute("src", item);*/
                        img.setAttribute("onclick", "myShare(\"" + document.location.protocol + "//" + document.location.hostname + "/" + "PhotoDetails?photoUrl=" + item + "\")");
                        img.setAttribute("src", "/images/facebook-share-button3.png");
                        var img2 = document.createElement("img");
                        /*               x.setAttribute("src", item);*/
                        img2.setAttribute("src", "/images/download-icon-red.webp");
                        var a2 = document.createElement("a");
                        /*               x.setAttribute("src", item);*/
                        a2.setAttribute("class", "download-ico");
                        a2.setAttribute("href", item);
                        a2.setAttribute("download", "");
                        a2.appendChild(img2);
                        a.appendChild(img);
                        main_pic.appendChild(a);
                        main_pic.appendChild(a2);
                        x.appendChild(main_pic);
                        document.getElementById("main-grid").appendChild(x);
                    }
                );

                //let event = new Event("load");
                //var elem = document.getElementsByTagName('main')[0];
                //elem.dispatchEvent(event);
                var evt = document.createEvent('Event');
                evt.initEvent('load', false, false);
                document.getElementsByTagName('main')[0].dispatchEvent(evt);
                //(function (d, s, id) {
                //    console.log("etrexa2");
                //    var js, fjs = d.getElementsByTagName(s)[0];
                //    if (d.getElementById(id)) {
                //        var sc = d.getElementById(id);
                //        sc.parentNode.removeChild(sc);
                //    } 
                //    js = d.createElement(s); js.id = id;
                //    js.src = "https://connect.facebook.net/en_US/sdk.js#xfbml=1&version=v3.0";
                //    fjs.parentNode.insertBefore(js, fjs);
                //}(document, 'script', 'facebook-jssdk'));
            }
        };
        //+ search + "&Skip" + indexCounter
        var counter = document.getElementById("index").value;
        counter++;
        /*        console.log("counter is "+counter);*/
        document.getElementById("index").value = counter;
        xhttp.open("GET", "/Index?handler=NewPictures&SearchString=" + search + "&Skip=" + document.getElementById("index").value, true);

        xhttp.send();

        //alert("conuter % 2 " +conuter % 2);
        //if (document.getElementById("counter") % 2 == 0) {
        //    alert("true");
        //}

    }
};

//$(window).scroll(function () {
//    const sum = $(window).scrollTop() + $(window).height() + 20;


//    alert($(window).scrollTop() + "and" + $(window).height() + "and" + $(document).height());

//    if (sum >= $(document).height()) {
//        var xhttp = new XMLHttpRequest();
//        xhttp.onreadystatechange = function () {


//            if (this.readyState == 4 && this.status == 200) {
//                //document.getElementById("demo").innerHTML =
//                var arr_from_json = JSON.parse(this.responseText);
//                arr_from_json.forEach(
//                    function (item, index) {
//                        alert(JSON.stringify(item) + "->" + index);
//                    }
//                );
//            }
//        };
//        xhttp.open("GET", "/Index?handler=NewPictures", true);
//        xhttp.send();

//        alert($(window).scrollTop() + "and" + $(window).height() + "and" + $(document).height());
//    }

//});

//$(document).ready(function () {
//    $(".main-pic-menu").hover(function () {
//        $(this).css("opacity", "100%");
//    }, function () {
//            $(this).css("opacity", "0%");
//    });
//});

$(function () {
    $("#txtLocation").autocomplete({
            source:
                function (request, response) {
                    $.ajax({
                        url: '/Index?handler=AutoComplete',
                        data: { "prefix": request.term },
                        type: "GET",
                        success: function (data) {
                            response($.map(data, function (item) {
                                return item;
                            }))
                        },
                        error: function (response) {
                        },
                        failure: function (response) {
                        }
                    });
                }
            ,
            //select: function (e, i) {
            //    $("#hfCustomer").val(i.item.val);
            //},
            minLength: 1
        });
});


var slideIndex = 1;
showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {

    // console.log(n);
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("dot");
    var texts = document.getElementsByClassName("text-slider");
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

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

