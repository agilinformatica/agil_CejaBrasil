using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ProtocoloAgil.Base.Models;

namespace MenorAprendizWeb.Base.Engine
{
    public class AprendizEngine
    {
        private readonly IRepository<Aprendiz> _repository;

        public AprendizEngine(IRepository<Aprendiz> repository)
        {
            _repository = repository;
        }

        public Aprendiz AtualizarAprendiz(Aprendiz aprendiz)
        {
            _repository.Edit(aprendiz);
            _repository.Save();
            return aprendiz;
        }

        public Aprendiz CriarAprendiz(Aprendiz aprendiz)
        {
            _repository.Add(aprendiz);
            _repository.Save();
            return aprendiz;
        }
    }
}
