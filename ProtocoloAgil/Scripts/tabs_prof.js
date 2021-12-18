/***************************/
//@Author: Adrian "yEnS" Mato Gondelle & Ivan Guardado Castro
//@website: www.yensdesign.com
//@email: yensamg@gmail.com
//@license: Feel free to use it, but keep this credits please!					
/***************************/

$(document).ready(function () {
    $(".menu > li").click(function (e) {
        $('#ContentHolder').fadeOut('slow', function () {
            $('#ContentFake').fadeIn();
        });

        switch (e.target.id) {

            case "geralprofessores":
                $("#geralprofessores").addClass("active");
                $("#notasprofessores").removeClass("active");
                $("#arquivosprofessores").removeClass("active");
                //display selected division, hide others
                $("div.geralprofessores").fadeIn();
                $("div.notasprofessores").css("display", "none");
                $("div.arquivosprofessores").css("display", "none");
                break;

            case "notasprofessores":
                $("#geralprofessores").removeClass("active");
                $("#notasprofessores").addClass("active");
                $("#arquivosprofessores").removeClass("active");
                //display selected division, hide others
                $("div.geralprofessores").css("display", "none");
                $("div.notasprofessores").fadeIn();
                $("div.arquivosprofessores").css("display", "none");
                break;

            case "arquivosprofessores":
                $("#geralprofessores").removeClass("active");
                $("#notasprofessores").removeClass("active");
                $("#arquivosprofessores").addClass("active");
                //display selected division, hide others
                $("div.geralprofessores").css("display", "none");
                $("div.notasprofessores").css("display", "none");
                $("div.arquivosprofessores").fadeIn();
                break;
        }
        //alert(e.target.id);
        return false;
    });
    $(".submenu > li").click(function (e) {
        $('#ContentFake').fadeOut('slow', function () {
            $('#ContentHolder').fadeIn();
        });
    });

});