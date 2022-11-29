$(document).ready(() => {

    new fullpage('#fullpage', {
        //options here
        autoScrolling: true,
        scrollHorizontally: false,
        scrollOverflowReset: true , 
        parallax:true
    })
    $("#lightslider").lightSlider({
        item: 4,
        autoWidth: false,
        loop: true,
        auto: true,
        rtl: true,
        enableTouch: true,
        enableDrag: true,
        pause: 3000,
        pager: true,
        mode: "slide",
        useCSS: true,
        cssEasing: 'ease',
        easing: 'linear',
        slideMargin: 15,
        controls: true,
        responsive: [{
                breakpoint: 1200,
                settings: {
                    item: 3,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    item: 2
                }
            },
            {
                breakpoint: 576,
                settings: {
                    item: 1
                }
            }
        ]
    })

    
    $("#firstPageSlider" ).lightSlider({
        item: 1,
        autoWidth: false,
        loop: true,
        auto: true,
        rtl: true,
        enableTouch: true,
        enableDrag: true,
        pause: 3000,
        pager: false,
        mode: "slide",
        useCSS: true,
        cssEasing: 'ease',
        easing: 'linear',
        slideMargin: 0,
        controls: false,

    })

})