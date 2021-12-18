using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;


namespace ProtocoloAgil.Base.Models
{
    public interface IRepository<T> where T : class
    {
        void Add(T e);
        void Remove(int id);
        T Find(int id);
        void Save();
        List<T> All();
        void Edit(T item);
    }

    public class Context<T> : DbContext where T : class
    {
        public Context() : base(GetConfig.Config())
        {
        }
        public DbSet<T> Itens { get; set; }
    }

    public class Repository<T> : IDisposable, IRepository<T> where T : class
    {
          protected readonly DbContext Context;
          public Repository(DbContext context)
          {
              Context = context;
          }

          public virtual void Add(T e)
          {
               Context.Set<T>().Add(e);
               Context.SaveChanges();
          }

          public virtual void Remove(int id)
          {
              var data = Context.Set<T>().Find(id);
            Context.Set<T>().Remove(data);
            Context.SaveChanges();
          }

          public virtual void Remove(int id01,int id02)
          {
              var data = Context.Set<T>().Find( id01,id02);
              Context.Set<T>().Remove(data);
              Context.SaveChanges();
          }

          public virtual void Remove(string id)
          {
              var data = Context.Set<T>().Find(id);
              Context.Set<T>().Remove(data);
              Context.SaveChanges();
          }

          public virtual void Remove(string id01,string id02)
          {
              var data = Context.Set<T>().Find(id01, id02);
              Context.Set<T>().Remove(data);
              Context.SaveChanges();
          }


          public virtual void Remove(T item)
          {
              Context.Set<T>().Remove(item);
              Context.SaveChanges();
          }

          public virtual T Find(int id)
          {
               return Context.Set<T>().Find(id);
          }

          public virtual T Find(T item)
          {
              return Context.Set<T>().Where(p => p == item).First();
          }

        public virtual T Find(string id)
          {
              return Context.Set<T>().Find(id);
          }

          public virtual T Find(int id01,int id02)
          {
              return Context.Set<T>().Find(id01, id02);
          }

          public virtual T Find(int id01, string id02)
          {
              return Context.Set<T>().Find(id01, id02);
          }


          public virtual IEnumerable<AulasProfessores> FindAulas(int ordem)
          {
             return Context.Set<AulasProfessores>().Where(p => p.ADPDisciplinaProf == ordem ).ToList();
          }

          public virtual DesempenhoAprendiz FindRegistro(int ordem, int aprendiz)
          {
              return Context.Set<DesempenhoAprendiz>().Where(p => p.DiaDisciplinaProf == ordem && p.DiaCodAprendiz == aprendiz).First();
          }


          public virtual T Find(int id01, int id02, int id03)
          {
              return Context.Set<T>().Find(id01, id02, id03);
          }


          public virtual T Find(int id01, int id02, int id03, int id04)
          {
              return Context.Set<T>().Find(id01, id02, id03, id04);
          }

          public virtual void Edit(T item)
          {
              Context.Entry(item).State = EntityState.Modified;
              Context.SaveChanges();
          }

          public virtual List<T> All()
          {
               var data = from i in Context.Set<T>() select i;
              return data.ToList();
          }

          public void Dispose()
          {
               Context.Dispose();
          }

          public void Save()
          {
              Context.SaveChanges();
          }
    }
}