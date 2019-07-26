document.onreadystatechange = function () {
    if (document.readyState === "complete") {
        preloader.style.display = "none";
    }
}

$(document).ready(function () {

    $('#mainSlider .owl-carousel').owlCarousel({
        loop: true,
        margin: 0,
        nav: false,
        dots: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    })

    $('.loop').owlCarousel({
        center: true,
        items: 1,
        loop: true,
        margin: 30,
        dots: false,
        responsive: {

            600: {
                items: 2
            }

        }
    });

    $(".sliderItem").click(function () {
        console.log("test")
        $(this).children(".videoImage").css({ "display": "none" });
        $(this).children(".videoSliderLayout").css({ "display": "none" });
        $(this).children(".iframeVideo").css({ "display": "block" })
    });

    $(".imgItem").click(function () {
        $(".showlayout").css({
            "opacity": "1",
            "z-index": "9999999"
        })
        $(this).children().attr("src")
        $(".showimg").attr("src", $(this).children().attr("src"))
        $(".close").click(function () {
            $(".showlayout").css({
                "opacity": "0",
                "z-index": "-99999"
            })
        })
    })

    $(".imgItem").click(function () {
        $(this).attr("data-index")
        $(".active").removeClass("active")
        $(".itemDiv").eq($(this).attr("data-index")).addClass("active")
    })

    $(".navButton").click(function () {
        $(".responsiveNav").slideToggle();
    })

    $(".studio").click(function () {
        $(".studioForm").slideToggle();
    })

    $(".photographs").click(function () {
        $(".photoForm").slideToggle();
    })

    $(".videographs").click(function () {
        $(".videoForm").slideToggle();
    })

    $("#orderClick").click(function () {
        $(".tablePart").slideToggle();
    })

    $("#blogClick").click(function () {
        $(".myBlogs").slideToggle();
    })

    $("#vacationClick").click(function () {
        $(".forVacations").slideToggle();
    })


});
