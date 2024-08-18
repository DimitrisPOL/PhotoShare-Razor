
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



var count = 0;
var indexCounter = 0;
window.onscroll = function (ev) {
    if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight - 50) {

        var search = document.getElementById("txtLocation").value;
        var images = document.getElementsByClassName("front-image");
        if (images != null) {

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
                var arr_from_json = JSON.parse(this.responseText);
                arr_from_json.forEach(
                    function (item, index) {

                        var uri = encodeURIComponent(item);
                        var html = "<a href=\"/Pictures/Details/" + uri + "\"><div class=\"main-pic\" ><img class=\"front-image\" src=\"" + item + "\" /></div></a>";
                        var x = document.createElement("div");

                        x.setAttribute("class", "main-pic-container");
                        x.innerHTML = html;
                        var main_pic = document.createElement("div");
                        main_pic.setAttribute("class", "main-pic-menu");
                        var a = document.createElement("a");
                        a.setAttribute("class", "share-ico");
                        a.setAttribute("href", "");

                        var img = document.createElement("img");
                        img.setAttribute("onclick", "myShare(\"" + document.location.protocol + "//" + document.location.hostname + "/" + "PhotoDetails?photoUrl=" + item + "\")");
                        img.setAttribute("src", "/images/facebook-share-button3.png");
                        var img2 = document.createElement("img");
                        img2.setAttribute("src", "/images/download-icon-red.webp");
                        var a2 = document.createElement("a");
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

                var evt = document.createEvent('Event');
                evt.initEvent('load', false, false);
                document.getElementsByTagName('main')[0].dispatchEvent(evt);
            }
        };
        var counter = document.getElementById("index").value;
        counter++;
        document.getElementById("index").value = counter;
        xhttp.open("GET", "/Index?handler=NewPictures&SearchString=" + search + "&Skip=" + document.getElementById("index").value, true);

        xhttp.send();
    }
};

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


function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

