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

            case "aprendiz":
                $("#aprendiz").addClass("active");
                $("#configuracoes").removeClass("active");
                $("#arquivosprofessores").removeClass("active");
                //display selected division, hide others
                $("div.aprendiz").fadeIn();
                $("div.configuracoes").css("display", "none");
                $("div.arquivosprofessores").css("display", "none");
                break;

            case "configuracoes":
                $("#aprendiz").removeClass("active");
                $("#configuracoes").addClass("active");
                $("#arquivosprofessores").removeClass("active");
                //display selected division, hide others
                $("div.aprendiz").css("display", "none");
                $("div.configuracoes").fadeIn();
                $("div.arquivosprofessores").css("display", "none");
                break;

            case "arquivosprofessores":
                $("#aprendiz").removeClass("active");
                $("#configuracoes").removeClass("active");
                $("#arquivosprofessores").addClass("active");
                //display selected division, hide others
                $("div.aprendiz").css("display", "none");
                $("div.configuracoes").css("display", "none");
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