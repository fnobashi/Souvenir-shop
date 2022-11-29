// wait till document loads completely ,,,
$(document).ready(function (){
    // define variables
    let items  =  document.querySelector(".souvenir-other-pictures ul").children;
    let totalItems = items.length;
    let selectedItem;

    // this function returns the index of next or previous item
    function  getSrc(dir){
        if (dir === "next") {
            selectedItem++
            if (selectedItem === totalItems) {
                selectedItem = 0;
            }
            return  selectedItem;
        }else if (dir === "prev")  {
            selectedItem--;
            if (selectedItem === 0){
                selectedItem = totalItems -1;
            }
            return  selectedItem;
        }
    }

    // events

    // open slider for the selected Image
    $(".souvenir-other-pictures img").click(function (e) {
        selectedItem = $(this).parent().index();
        $("#picture-slider .image img#slider-image").attr("src" ,e.target.getAttribute("src"));
        $("#picture-slider").css("display" , "flex");

    });

    // go to next picture
    $("#picture-slider #btn-frw").click(function (e){
        let i = getSrc("next");
        $("#picture-slider .image img#slider-image").attr("src" , items[i].children[0].getAttribute("src"));
    });

    // go to previous picture
    $("#picture-slider #btn-prev").click(function (){
        let i = getSrc("prev");
        $("#picture-slider .image img#slider-image").attr("src" , items[i].children[0].getAttribute("src"));
    });

    // close the slider
    $("#picture-slider .btn-close").click(function () {
       $("#picture-slider").hide();
    });
});