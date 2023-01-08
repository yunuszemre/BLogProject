using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OnionArcBLogProject.Core.Entity;

namespace OnionArcBLogProject.Core.Service
{
    public interface ICoreService<T> where T : CoreEntity
    {
        T GetById(int id);

        bool Update(T item);

        bool Remove(T item);

        bool Remove(Guid id);

        bool RemoveAll(Expression<Func<T, bool>> exp);

        bool Add(T item);

        bool Add(List<T> items);

        bool DeleteAll(Expression<Func<T, bool>> exp); //Belirli bir Linq ifadesine göre filtreleyip silmek için yazılan servis methodu

        T GetById(Guid id);

        T GetByDefault(Expression<Func<T, bool>> exp);

        List<T> GetActive();
        IQueryable<T> GetActive(params Expression<Func<T, object>>[] include);

        List<T> GetDefault(Expression<Func<T, bool>> exp);

        List<T> GetAll();
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] include);

        bool Activate(Guid id);

        bool Any(Expression<Func<T, bool>> exp);

        int Save();

    }
}
