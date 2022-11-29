$(document).ready(function(){
    $(document).on("click" , ".responsive-button" , function (){
       $(".responsive-menu").css("right" , "0");
    });

    $("#responsive-menu-close-btn").click(function (){
        $(".responsive-menu").css("right" , "-250px");
    })


});