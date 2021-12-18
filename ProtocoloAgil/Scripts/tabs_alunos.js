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

            case "secretariaalunos":
                $("#secretariaalunos").addClass("active");
                $("#academicoalunos").removeClass("active");
                $("#arquivosalunos").removeClass("active");
                //display selected division, hide others
                $("div.secretariaalunos").fadeIn();
                $("div.academicoalunos").css("display", "none");
                $("div.arquivosalunos").css("display", "none");
                break;

            case "academicoalunos":
                $("#secretariaalunos").removeClass("active");
                $("#academicoalunos").addClass("active");
                $("#arquivosalunos").removeClass("active");
                //display selected division, hide others
                $("div.secretariaalunos").css("display", "none");
                $("div.academicoalunos").fadeIn();
                $("div.arquivosalunos").css("display", "none");
                break;

            case "arquivosalunos":
                $("#secretariaalunos").removeClass("active");
                $("#academicoalunos").removeClass("active");
                $("#arquivosalunos").addClass("active");
                //display selected division, hide others
                $("div.secretariaalunos").css("display", "none");
                $("div.academicoalunos").css("display", "none");
                $("div.arquivosalunos").fadeIn();
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