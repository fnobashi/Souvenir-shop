$(document).ready(function () {
    let currentmousepos = { x: -1, y: -1 }

    $(document).mousemove(function (event) {
        currentmousepos.x = event.pageX;
        currentmousepos.y = event.pageY;
        let elementXpos = currentmousepos.x +20;
        let elementYpos = currentmousepos.y +20;
        if (event.target.matches("[data-province]")) {
            let provinceName = event.target.attributes.getNamedItem("data-province").value;
            let categoriesCount = event.target.attributes.getNamedItem("data-category").value;
            let souvenirsCount = event.target.attributes.getNamedItem("data-souvenirs").value;
            $("#map-information-text").html("<p>" + provinceName + "<p/>" + "<br/>").append("<p>" + "دسته بندی های استان : " + categoriesCount + "<p/>" + "<br/>").append("<p>" + "تعداد محصولات استان : " + souvenirsCount + "<p/>" + "<br/>");
            $("#map-information").css("display", "flex");
            $("#map-information").css("top", elementYpos + "px");
            $("#map-information").css("left", elementXpos + "px");
        } else {
            $("#map-information").css("display", "none");
        }
    });
});