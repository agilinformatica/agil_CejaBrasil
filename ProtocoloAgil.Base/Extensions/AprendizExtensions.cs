using MenorAprendizWeb.Base.ViewModel;
using ProtocoloAgil.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MenorAprendizWeb.Base.Engine
{
    public static class AprendizExtensions
    {

        public static Aprendiz AtualizaAprendiz(this Aprendiz aprendiz, ViewModelAprendiz viewModelAprendiz)
        {
           // aprendiz.Apr_Nome = viewModelAprendiz.Nome;
         //   aprendiz.Apr_Sexo = viewModelAprendiz.Sexo;
            return aprendiz;
        }

        public static Aprendiz AtualizaAprendiz(this Aprendiz aprendiz, ViewModelAprendizSocioEconomico viewModelAprendizSocioEconomico)
        {

            return aprendiz;
        }
    }
}
