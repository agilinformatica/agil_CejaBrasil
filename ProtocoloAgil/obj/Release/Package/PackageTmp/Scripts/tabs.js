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
            case "configuracoes":
                //change status & style menu
                $("#configuracoes").addClass("active");
                $("#aprendiz").removeClass("active");
                $("#pedagogico").removeClass("active");
                $("#estatisticas").removeClass("active");
                //display selected division, hide others
                $("div.configuracoes").fadeIn();
                $("div.aprendiz").css("display", "none");
                $("div.pedagogico").css("display", "none");
                $("div.estatisticas").css("display", "none");

                break;
            case "aprendiz":
                //change status & style menu
                $("#aprendiz").addClass("active");
                $("#configuracoes").removeClass("active");
                $("#pedagogico").removeClass("active");
                $("#estatisticas").removeClass("active");
                //display selected division, hide others
                $("div.aprendiz").fadeIn();
                $("div.configuracoes").css("display", "none");
                $("div.pedagogico").css("display", "none");
                $("div.estatisticas").css("display", "none");

                break;
          
            case "pedagogico":
                //change status & style menu
                $("#pedagogico").addClass("active");
                $("#configuracoes").removeClass("active");
                $("#aprendiz").removeClass("active");
                $("#estatisticas").removeClass("active");
                //display selected division, hide others
                $("div.pedagogico").fadeIn();
                $("div.configuracoes").css("display", "none");
                $("div.aprendiz").css("display", "none");
                $("div.estatisticas").css("display", "none");

                break;
                
            case "estatisticas":
                //change status & style menu
                $("#estatisticas").addClass("active");
                $("#configuracoes").removeClass("active");
                $("#aprendiz").removeClass("active");
                $("#pedagogico").removeClass("active");
                //display selected division, hide others
                $("div.estatisticas").fadeIn();
                $("div.configuracoes").css("display", "none");
                $("div.aprendiz").css("display", "none");
                $("div.pedagogico").css("display", "none");

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