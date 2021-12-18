using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;
using System.Web;

namespace ProtocoloAgil.pages
{

    public partial class CalendarioAprendiz : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

     

            // MÊS 1
            HFData1Mes1.Value = "1900-01-01";
            HFData2Mes1.Value = "1900-01-01";
            HFData3Mes1.Value = "1900-01-01";
            HFData4Mes1.Value = "1900-01-01";
            HFData5Mes1.Value = "1900-01-01";
            HFData6Mes1.Value = "1900-01-01";
            HFData7Mes1.Value = "1900-01-01";
            HFData8Mes1.Value = "1900-01-01";
            HFData9Mes1.Value = "1900-01-01";
            HFData10Mes1.Value = "1900-01-01";
            HFData11Mes1.Value = "1900-01-01";
            HFData12Mes1.Value = "1900-01-01";
            HFData13Mes1.Value = "1900-01-01";
            HFData14Mes1.Value = "1900-01-01";
            HFData15Mes1.Value = "1900-01-01";
            HFData16Mes1.Value = "1900-01-01";
            HFData17Mes1.Value = "1900-01-01";
            HFData18Mes1.Value = "1900-01-01";
            HFData19Mes1.Value = "1900-01-01";
            HFData20Mes1.Value = "1900-01-01";


            // MÊS 2
            HFData1Mes2.Value = "1900-01-01";
            HFData2Mes2.Value = "1900-01-01";
            HFData3Mes2.Value = "1900-01-01";
            HFData4Mes2.Value = "1900-01-01";
            HFData5Mes2.Value = "1900-01-01";
            HFData6Mes2.Value = "1900-01-01";
            HFData7Mes2.Value = "1900-01-01";
            HFData8Mes2.Value = "1900-01-01";
            HFData9Mes2.Value = "1900-01-01";
            HFData10Mes2.Value = "1900-01-01";
            HFData11Mes2.Value = "1900-01-01";
            HFData12Mes2.Value = "1900-01-01";
            HFData13Mes2.Value = "1900-01-01";
            HFData14Mes2.Value = "1900-01-01";
            HFData15Mes2.Value = "1900-01-01";
            HFData16Mes2.Value = "1900-01-01";
            HFData17Mes2.Value = "1900-01-01";
            HFData18Mes2.Value = "1900-01-01";
            HFData19Mes2.Value = "1900-01-01";
            HFData20Mes2.Value = "1900-01-01";


            // MÊS 3
            HFData1Mes3.Value = "1900-01-01";
            HFData2Mes3.Value = "1900-01-01";
            HFData3Mes3.Value = "1900-01-01";
            HFData4Mes3.Value = "1900-01-01";
            HFData5Mes3.Value = "1900-01-01";
            HFData6Mes3.Value = "1900-01-01";
            HFData7Mes3.Value = "1900-01-01";
            HFData8Mes3.Value = "1900-01-01";
            HFData9Mes3.Value = "1900-01-01";
            HFData10Mes3.Value = "1900-01-01";
            HFData11Mes3.Value = "1900-01-01";
            HFData12Mes3.Value = "1900-01-01";
            HFData13Mes3.Value = "1900-01-01";
            HFData14Mes3.Value = "1900-01-01";
            HFData15Mes3.Value = "1900-01-01";
            HFData16Mes3.Value = "1900-01-01";
            HFData17Mes3.Value = "1900-01-01";
            HFData18Mes3.Value = "1900-01-01";
            HFData19Mes3.Value = "1900-01-01";
            HFData20Mes3.Value = "1900-01-01";



            // MÊS 4
            HFData1Mes4.Value = "1900-01-01";
            HFData2Mes4.Value = "1900-01-01";
            HFData3Mes4.Value = "1900-01-01";
            HFData4Mes4.Value = "1900-01-01";
            HFData5Mes4.Value = "1900-01-01";
            HFData6Mes4.Value = "1900-01-01";
            HFData7Mes4.Value = "1900-01-01";
            HFData8Mes4.Value = "1900-01-01";
            HFData9Mes4.Value = "1900-01-01";
            HFData10Mes4.Value = "1900-01-01";
            HFData11Mes4.Value = "1900-01-01";
            HFData12Mes4.Value = "1900-01-01";
            HFData13Mes4.Value = "1900-01-01";
            HFData14Mes4.Value = "1900-01-01";
            HFData15Mes4.Value = "1900-01-01";
            HFData16Mes4.Value = "1900-01-01";
            HFData17Mes4.Value = "1900-01-01";
            HFData18Mes4.Value = "1900-01-01";
            HFData19Mes4.Value = "1900-01-01";
            HFData20Mes4.Value = "1900-01-01";



            // MÊS 5
            HFData1Mes5.Value = "1900-01-01";
            HFData2Mes5.Value = "1900-01-01";
            HFData3Mes5.Value = "1900-01-01";
            HFData4Mes5.Value = "1900-01-01";
            HFData5Mes5.Value = "1900-01-01";
            HFData6Mes5.Value = "1900-01-01";
            HFData7Mes5.Value = "1900-01-01";
            HFData8Mes5.Value = "1900-01-01";
            HFData9Mes5.Value = "1900-01-01";
            HFData10Mes5.Value = "1900-01-01";
            HFData11Mes5.Value = "1900-01-01";
            HFData12Mes5.Value = "1900-01-01";
            HFData13Mes5.Value = "1900-01-01";
            HFData14Mes5.Value = "1900-01-01";
            HFData15Mes5.Value = "1900-01-01";
            HFData16Mes5.Value = "1900-01-01";
            HFData17Mes5.Value = "1900-01-01";
            HFData18Mes5.Value = "1900-01-01";
            HFData19Mes5.Value = "1900-01-01";
            HFData20Mes5.Value = "1900-01-01";



            // MÊS 6
            HFData1Mes6.Value = "1900-01-01";
            HFData2Mes6.Value = "1900-01-01";
            HFData3Mes6.Value = "1900-01-01";
            HFData4Mes6.Value = "1900-01-01";
            HFData5Mes6.Value = "1900-01-01";
            HFData6Mes6.Value = "1900-01-01";
            HFData7Mes6.Value = "1900-01-01";
            HFData8Mes6.Value = "1900-01-01";
            HFData9Mes6.Value = "1900-01-01";
            HFData10Mes6.Value = "1900-01-01";
            HFData11Mes6.Value = "1900-01-01";
            HFData12Mes6.Value = "1900-01-01";
            HFData13Mes6.Value = "1900-01-01";
            HFData14Mes6.Value = "1900-01-01";
            HFData15Mes6.Value = "1900-01-01";
            HFData16Mes6.Value = "1900-01-01";
            HFData17Mes6.Value = "1900-01-01";
            HFData18Mes6.Value = "1900-01-01";
            HFData19Mes6.Value = "1900-01-01";
            HFData20Mes6.Value = "1900-01-01";


            
            // MÊS 7
            HFData1Mes7.Value = "1900-01-01";
            HFData2Mes7.Value = "1900-01-01";
            HFData3Mes7.Value = "1900-01-01";
            HFData4Mes7.Value = "1900-01-01";
            HFData5Mes7.Value = "1900-01-01";
            HFData6Mes7.Value = "1900-01-01";
            HFData7Mes7.Value = "1900-01-01";
            HFData8Mes7.Value = "1900-01-01";
            HFData9Mes7.Value = "1900-01-01";
            HFData10Mes7.Value = "1900-01-01";
            HFData11Mes7.Value = "1900-01-01";
            HFData12Mes7.Value = "1900-01-01";
            HFData13Mes7.Value = "1900-01-01";
            HFData14Mes7.Value = "1900-01-01";
            HFData15Mes7.Value = "1900-01-01";
            HFData16Mes7.Value = "1900-01-01";
            HFData17Mes7.Value = "1900-01-01";
            HFData18Mes7.Value = "1900-01-01";
            HFData19Mes7.Value = "1900-01-01";
            HFData20Mes7.Value = "1900-01-01";



            // MÊS 8
            HFData1Mes8.Value = "1900-01-01";
            HFData2Mes8.Value = "1900-01-01";
            HFData3Mes8.Value = "1900-01-01";
            HFData4Mes8.Value = "1900-01-01";
            HFData5Mes8.Value = "1900-01-01";
            HFData6Mes8.Value = "1900-01-01";
            HFData7Mes8.Value = "1900-01-01";
            HFData8Mes8.Value = "1900-01-01";
            HFData9Mes8.Value = "1900-01-01";
            HFData10Mes8.Value = "1900-01-01";
            HFData11Mes8.Value = "1900-01-01";
            HFData12Mes8.Value = "1900-01-01";
            HFData13Mes8.Value = "1900-01-01";
            HFData14Mes8.Value = "1900-01-01";
            HFData15Mes8.Value = "1900-01-01";
            HFData16Mes8.Value = "1900-01-01";
            HFData17Mes8.Value = "1900-01-01";
            HFData18Mes8.Value = "1900-01-01";
            HFData19Mes8.Value = "1900-01-01";
            HFData20Mes8.Value = "1900-01-01";



            // MÊS 9
            HFData1Mes9.Value = "1900-01-01";
            HFData2Mes9.Value = "1900-01-01";
            HFData3Mes9.Value = "1900-01-01";
            HFData4Mes9.Value = "1900-01-01";
            HFData5Mes9.Value = "1900-01-01";
            HFData6Mes9.Value = "1900-01-01";
            HFData7Mes9.Value = "1900-01-01";
            HFData8Mes9.Value = "1900-01-01";
            HFData9Mes9.Value = "1900-01-01";
            HFData10Mes9.Value = "1900-01-01";
            HFData11Mes9.Value = "1900-01-01";
            HFData12Mes9.Value = "1900-01-01";
            HFData13Mes9.Value = "1900-01-01";
            HFData14Mes9.Value = "1900-01-01";
            HFData15Mes9.Value = "1900-01-01";
            HFData16Mes9.Value = "1900-01-01";
            HFData17Mes9.Value = "1900-01-01";
            HFData18Mes9.Value = "1900-01-01";
            HFData19Mes9.Value = "1900-01-01";
            HFData20Mes9.Value = "1900-01-01";



            // MÊS 10
            HFData1Mes10.Value = "1900-01-01";
            HFData2Mes10.Value = "1900-01-01";
            HFData3Mes10.Value = "1900-01-01";
            HFData4Mes10.Value = "1900-01-01";
            HFData5Mes10.Value = "1900-01-01";
            HFData6Mes10.Value = "1900-01-01";
            HFData7Mes10.Value = "1900-01-01";
            HFData8Mes10.Value = "1900-01-01";
            HFData9Mes10.Value = "1900-01-01";
            HFData10Mes10.Value = "1900-01-01";
            HFData11Mes10.Value = "1900-01-01";
            HFData12Mes10.Value = "1900-01-01";
            HFData13Mes10.Value = "1900-01-01";
            HFData14Mes10.Value = "1900-01-01";
            HFData15Mes10.Value = "1900-01-01";
            HFData16Mes10.Value = "1900-01-01";
            HFData17Mes10.Value = "1900-01-01";
            HFData18Mes10.Value = "1900-01-01";
            HFData19Mes10.Value = "1900-01-01";
            HFData20Mes10.Value = "1900-01-01";


            // MÊS 11
            HFData1Mes11.Value = "1900-01-01";
            HFData2Mes11.Value = "1900-01-01";
            HFData3Mes11.Value = "1900-01-01";
            HFData4Mes11.Value = "1900-01-01";
            HFData5Mes11.Value = "1900-01-01";
            HFData6Mes11.Value = "1900-01-01";
            HFData7Mes11.Value = "1900-01-01";
            HFData8Mes11.Value = "1900-01-01";
            HFData9Mes11.Value = "1900-01-01";
            HFData10Mes11.Value = "1900-01-01";
            HFData11Mes11.Value = "1900-01-01";
            HFData12Mes11.Value = "1900-01-01";
            HFData13Mes11.Value = "1900-01-01";
            HFData14Mes11.Value = "1900-01-01";
            HFData15Mes11.Value = "1900-01-01";
            HFData16Mes11.Value = "1900-01-01";
            HFData17Mes11.Value = "1900-01-01";
            HFData18Mes11.Value = "1900-01-01";
            HFData19Mes11.Value = "1900-01-01";
            HFData20Mes11.Value = "1900-01-01";


            // MÊS 12
            HFData1Mes12.Value = "1900-01-01";
            HFData2Mes12.Value = "1900-01-01";
            HFData3Mes12.Value = "1900-01-01";
            HFData4Mes12.Value = "1900-01-01";
            HFData5Mes12.Value = "1900-01-01";
            HFData6Mes12.Value = "1900-01-01";
            HFData7Mes12.Value = "1900-01-01";
            HFData8Mes12.Value = "1900-01-01";
            HFData9Mes12.Value = "1900-01-01";
            HFData10Mes12.Value = "1900-01-01";
            HFData11Mes12.Value = "1900-01-01";
            HFData12Mes12.Value = "1900-01-01";
            HFData13Mes12.Value = "1900-01-01";
            HFData14Mes12.Value = "1900-01-01";
            HFData15Mes12.Value = "1900-01-01";
            HFData16Mes12.Value = "1900-01-01";
            HFData17Mes12.Value = "1900-01-01";
            HFData18Mes12.Value = "1900-01-01";
            HFData19Mes12.Value = "1900-01-01";
            HFData20Mes12.Value = "1900-01-01";



            // MÊS 13
            HFData1Mes13.Value = "1900-01-01";
            HFData2Mes13.Value = "1900-01-01";
            HFData3Mes13.Value = "1900-01-01";
            HFData4Mes13.Value = "1900-01-01";
            HFData5Mes13.Value = "1900-01-01";
            HFData6Mes13.Value = "1900-01-01";
            HFData7Mes13.Value = "1900-01-01";
            HFData8Mes13.Value = "1900-01-01";
            HFData9Mes13.Value = "1900-01-01";
            HFData10Mes13.Value = "1900-01-01";
            HFData11Mes13.Value = "1900-01-01";
            HFData12Mes13.Value = "1900-01-01";
            HFData13Mes13.Value = "1900-01-01";
            HFData14Mes13.Value = "1900-01-01";
            HFData15Mes13.Value = "1900-01-01";
            HFData16Mes13.Value = "1900-01-01";
            HFData17Mes13.Value = "1900-01-01";
            HFData18Mes13.Value = "1900-01-01";
            HFData19Mes13.Value = "1900-01-01";
            HFData20Mes13.Value = "1900-01-01";



            // MÊS 14
            HFData1Mes14.Value = "1900-01-01";
            HFData2Mes14.Value = "1900-01-01";
            HFData3Mes14.Value = "1900-01-01";
            HFData4Mes14.Value = "1900-01-01";
            HFData5Mes14.Value = "1900-01-01";
            HFData6Mes14.Value = "1900-01-01";
            HFData7Mes14.Value = "1900-01-01";
            HFData8Mes14.Value = "1900-01-01";
            HFData9Mes14.Value = "1900-01-01";
            HFData10Mes14.Value = "1900-01-01";
            HFData11Mes14.Value = "1900-01-01";
            HFData12Mes14.Value = "1900-01-01";
            HFData13Mes14.Value = "1900-01-01";
            HFData14Mes14.Value = "1900-01-01";
            HFData15Mes14.Value = "1900-01-01";
            HFData16Mes14.Value = "1900-01-01";
            HFData17Mes14.Value = "1900-01-01";
            HFData18Mes14.Value = "1900-01-01";
            HFData19Mes14.Value = "1900-01-01";
            HFData20Mes14.Value = "1900-01-01";



            // MÊS 15
            HFData1Mes15.Value = "1900-01-01";
            HFData2Mes15.Value = "1900-01-01";
            HFData3Mes15.Value = "1900-01-01";
            HFData4Mes15.Value = "1900-01-01";
            HFData5Mes15.Value = "1900-01-01";
            HFData6Mes15.Value = "1900-01-01";
            HFData7Mes15.Value = "1900-01-01";
            HFData8Mes15.Value = "1900-01-01";
            HFData9Mes15.Value = "1900-01-01";
            HFData10Mes15.Value = "1900-01-01";
            HFData11Mes15.Value = "1900-01-01";
            HFData12Mes15.Value = "1900-01-01";
            HFData13Mes15.Value = "1900-01-01";
            HFData14Mes15.Value = "1900-01-01";
            HFData15Mes15.Value = "1900-01-01";
            HFData16Mes15.Value = "1900-01-01";
            HFData17Mes15.Value = "1900-01-01";
            HFData18Mes15.Value = "1900-01-01";
            HFData19Mes15.Value = "1900-01-01";
            HFData20Mes15.Value = "1900-01-01";

            // MÊS 16
            HFData1Mes16.Value = "1900-01-01";
            HFData2Mes16.Value = "1900-01-01";
            HFData3Mes16.Value = "1900-01-01";
            HFData4Mes16.Value = "1900-01-01";
            HFData5Mes16.Value = "1900-01-01";
            HFData6Mes16.Value = "1900-01-01";
            HFData7Mes16.Value = "1900-01-01";
            HFData8Mes16.Value = "1900-01-01";
            HFData9Mes16.Value = "1900-01-01";
            HFData10Mes16.Value = "1900-01-01";
            HFData11Mes16.Value = "1900-01-01";
            HFData12Mes16.Value = "1900-01-01";
            HFData13Mes16.Value = "1900-01-01";
            HFData14Mes16.Value = "1900-01-01";
            HFData15Mes16.Value = "1900-01-01";
            HFData16Mes16.Value = "1900-01-01";
            HFData17Mes16.Value = "1900-01-01";
            HFData18Mes16.Value = "1900-01-01";
            HFData19Mes16.Value = "1900-01-01";
            HFData20Mes16.Value = "1900-01-01";


            // MÊS 17
            HFData1Mes17.Value = "1900-01-01";
            HFData2Mes17.Value = "1900-01-01";
            HFData3Mes17.Value = "1900-01-01";
            HFData4Mes17.Value = "1900-01-01";
            HFData5Mes17.Value = "1900-01-01";
            HFData6Mes17.Value = "1900-01-01";
            HFData7Mes17.Value = "1900-01-01";
            HFData8Mes17.Value = "1900-01-01";
            HFData9Mes17.Value = "1900-01-01";
            HFData10Mes17.Value = "1900-01-01";
            HFData11Mes17.Value = "1900-01-01";
            HFData12Mes17.Value = "1900-01-01";
            HFData13Mes17.Value = "1900-01-01";
            HFData14Mes17.Value = "1900-01-01";
            HFData15Mes17.Value = "1900-01-01";
            HFData16Mes17.Value = "1900-01-01";
            HFData17Mes17.Value = "1900-01-01";
            HFData18Mes17.Value = "1900-01-01";
            HFData19Mes17.Value = "1900-01-01";
            HFData20Mes17.Value = "1900-01-01";


            // MÊS 18
            HFData1Mes18.Value = "1900-01-01";
            HFData2Mes18.Value = "1900-01-01";
            HFData3Mes18.Value = "1900-01-01";
            HFData4Mes18.Value = "1900-01-01";
            HFData5Mes18.Value = "1900-01-01";
            HFData6Mes18.Value = "1900-01-01";
            HFData7Mes18.Value = "1900-01-01";
            HFData8Mes18.Value = "1900-01-01";
            HFData9Mes18.Value = "1900-01-01";
            HFData10Mes18.Value = "1900-01-01";
            HFData11Mes18.Value = "1900-01-01";
            HFData12Mes18.Value = "1900-01-01";
            HFData13Mes18.Value = "1900-01-01";
            HFData14Mes18.Value = "1900-01-01";
            HFData15Mes18.Value = "1900-01-01";
            HFData16Mes18.Value = "1900-01-01";
            HFData17Mes18.Value = "1900-01-01";
            HFData18Mes18.Value = "1900-01-01";
            HFData19Mes18.Value = "1900-01-01";
            HFData20Mes18.Value = "1900-01-01";


            //MesPadrao1.Value = "1900-1";
            //MesPadrao2.Value = "1900-1";
            //MesPadrao3.Value = "1900-1";
            //MesPadrao4.Value = "1900-1";
            //MesPadrao5.Value = "1900-1";
            //MesPadrao6.Value = "1900-1";
            //MesPadrao7.Value = "1900-1";
            //MesPadrao8.Value = "1900-1";
            //MesPadrao9.Value = "1900-1";
            //MesPadrao10.Value = "1900-1";
            //MesPadrao11.Value = "1900-1";
            //MesPadrao12.Value = "1900-1";
            //MesPadrao13.Value = "1900-1";
            //MesPadrao14.Value = "1900-1";
            //MesPadrao15.Value = "1900-1";

            string id = Request.QueryString["id"];

            string data;

            var sql = "SELECT CA_AulasDisciplinasAprendiz.AdiCodAprendiz, datepart(yy, AdiDataAula) AS AnoRef,datepart(mm, AdiDataAula) AS MesRef, Min(CA_AulasDisciplinasAprendiz.AdiDataAula) AS MínDeAdiDataAula, Max(CA_AulasDisciplinasAprendiz.AdiDataAula) AS MaxDeAdiDataAula FROM CA_AulasDisciplinasAprendiz GROUP BY CA_AulasDisciplinasAprendiz.AdiCodAprendiz, datepart(mm, AdiDataAula), datepart(yy, AdiDataAula) HAVING CA_AulasDisciplinasAprendiz.AdiCodAprendiz=" + id + "";
            var con = new Conexao();
            var result = con.Consultar(sql);
            int i = 1;
            int mudaMes = 1;
            string mes = "1";
            while (result.Read())
            {
                if (mudaMes == 1)
                {
                    MesPadrao1.Value = result["AnoRef"] + "-" + result["MesRef"];
                }

                if (mudaMes == 2)
                {
                    MesPadrao2.Value = result["AnoRef"] + "-" + result["MesRef"];
                }

                if (mudaMes == 3)
                {
                    MesPadrao3.Value = result["AnoRef"] + "-" + result["MesRef"];
                }


                if (mudaMes == 4)
                {
                    MesPadrao4.Value = result["AnoRef"] + "-" + result["MesRef"];
                }

                if (mudaMes == 5)
                {
                    MesPadrao5.Value = result["AnoRef"] + "-" + result["MesRef"];
                }

                if (mudaMes == 6)
                {
                    MesPadrao6.Value = result["AnoRef"] + "-" + result["MesRef"];
                }


                if (mudaMes == 7)
                {
                    MesPadrao7.Value = result["AnoRef"] + "-" + result["MesRef"];
                }


                if (mudaMes == 8)
                {
                    MesPadrao8.Value = result["AnoRef"] + "-" + result["MesRef"];
                }

                if (mudaMes == 9)
                {
                    MesPadrao9.Value = result["AnoRef"] + "-" + result["MesRef"];
                }

                if (mudaMes == 10)
                {
                    MesPadrao10.Value = result["AnoRef"] + "-" + result["MesRef"];
                }

                if (mudaMes == 11)
                {
                    MesPadrao11.Value = result["AnoRef"] + "-" + result["MesRef"];
                }


                if (mudaMes == 12)
                {
                    MesPadrao12.Value = result["AnoRef"] + "-" + result["MesRef"];
                }

                if (mudaMes == 13)
                {
                    MesPadrao13.Value = result["AnoRef"] + "-" + result["MesRef"];
                }


                if (mudaMes == 14)
                {
                    MesPadrao14.Value = result["AnoRef"] + "-" + result["MesRef"];
                }

                if (mudaMes == 15)
                {
                    MesPadrao15.Value = result["AnoRef"] + "-" + result["MesRef"];
                }

                if (mudaMes == 16)
                {
                    MesPadrao16.Value = result["AnoRef"] + "-" + result["MesRef"];
                }

                if (mudaMes == 17)
                {
                    MesPadrao17.Value = result["AnoRef"] + "-" + result["MesRef"];
                }

                if (mudaMes == 18)
                {
                    MesPadrao17.Value = result["AnoRef"] + "-" + result["MesRef"];
                }


                var sql2 = "SELECT CA_AulasDisciplinasAprendiz.AdiCodAprendiz, datepart(yy, AdiDataAula) AS AnoRef,datepart(mm, AdiDataAula) AS MesRef, CA_AulasDisciplinasAprendiz.AdiDataAula, Apr_Nome,Apr_inicioAprendizagem, Apr_prevFimAprendizagem, TurNome, TurObservacoes FROM CA_AulasDisciplinasAprendiz join CA_Aprendiz on CA_AulasDisciplinasAprendiz.AdiCodAprendiz = CA_Aprendiz.Apr_Codigo join CA_Turmas on CA_Aprendiz.Apr_Turma = CA_Turmas.TurCodigo WHERE CA_AulasDisciplinasAprendiz.AdiCodAprendiz=" + id + " and AdiDataAula BETWEEN '" + result["MínDeAdiDataAula"] + "' and '" + result["MaxDeAdiDataAula"] + "' ORDER BY datepart(yy, AdiDataAula) ,datepart(mm, AdiDataAula) , CA_AulasDisciplinasAprendiz.AdiDataAula";
                var con2 = new Conexao();
                var result2 = con2.Consultar(sql2);
                while (result2.Read())
                {
                    lblEmpresa.Text = "Ceja Brasil";
                    lblNome.Text = "Aprendiz: " + result2["Apr_Nome"].ToString();
                    lblTurma.Text = "Turma: " + result2["TurNome"].ToString() + "  " + result2["TurObservacoes"].ToString();
                    lblInicio.Text = "Periodo de Aprendizagem de: " + string.Format("{0:dd/MM/yyyy}", (DateTime)result2["Apr_InicioAprendizagem"]);
                    lblTermino.Text = "a " + string.Format("{0:dd/MM/yyyy}", (DateTime)result2["Apr_PrevFimAprendizagem"]); 

                    if (mudaMes == 1)
                    {
                        if (i == 1)
                        {
                            HFData1Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes1.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }

                    if (mudaMes == 2)
                    {
                        if (i == 1)
                        {
                            HFData1Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes2.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }


                    if (mudaMes == 3)
                    {
                        if (i == 1)
                        {
                            HFData1Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes3.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }


                    if (mudaMes == 4)
                    {
                        if (i == 1)
                        {
                            HFData1Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes4.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }



                    if (mudaMes == 5)
                    {
                        if (i == 1)
                        {
                            HFData1Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes5.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }



                    if (mudaMes == 6)
                    {
                        if (i == 1)
                        {
                            HFData1Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes6.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }





                    if (mudaMes == 7)
                    {
                        if (i == 1)
                        {
                            HFData1Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes7.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }




                    if (mudaMes == 8)
                    {
                        if (i == 1)
                        {
                            HFData1Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes8.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }





                    if (mudaMes == 9)
                    {
                        if (i == 1)
                        {
                            HFData1Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData9Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes9.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }







                    if (mudaMes == 10)
                    {
                        if (i == 1)
                        {
                            HFData1Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes10.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }








                    if (mudaMes == 11)
                    {
                        if (i == 1)
                        {
                            HFData1Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes11.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }







                    if (mudaMes == 12)
                    {
                        if (i == 1)
                        {
                            HFData1Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes12.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }







                    if (mudaMes == 13)
                    {
                        if (i == 1)
                        {
                            HFData1Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes13.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }









                    if (mudaMes == 14)
                    {
                        if (i == 1)
                        {
                            HFData1Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes14.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }










                    if (mudaMes == 15)
                    {
                        if (i == 1)
                        {
                            HFData1Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes15.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }








                    if (mudaMes == 16)
                    {
                        if (i == 1)
                        {
                            HFData1Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes16.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }


                    if (mudaMes == 17)
                    {
                        if (i == 1)
                        {
                            HFData1Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes17.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }



                    if (mudaMes == 18)
                    {
                        if (i == 1)
                        {
                            HFData1Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 2)
                        {
                            HFData2Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 3)
                        {
                            HFData3Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 4)
                        {
                            HFData4Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 5)
                        {
                            HFData5Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);

                        }
                        else if (i == 6)
                        {
                            HFData6Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 7)
                        {
                            HFData7Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 8)
                        {
                            HFData8Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 9)
                        {
                            HFData9Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 10)
                        {
                            HFData10Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 11)
                        {
                            HFData11Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 12)
                        {
                            HFData12Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 13)
                        {
                            HFData13Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 14)
                        {
                            HFData14Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 15)
                        {
                            HFData15Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 16)
                        {
                            HFData16Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 17)
                        {
                            HFData17Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 18)
                        {
                            HFData18Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 19)
                        {
                            HFData19Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                        else if (i == 20)
                        {
                            HFData20Mes18.Value = string.Format("{0:yyyy-MM-dd}", (DateTime)result2["AdiDataAula"]);
                        }
                    }



                    i++;
                }
                mudaMes++;
                i = 1;



            }
        }
    }
}