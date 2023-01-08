using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using OnionArcBLogProject.Core.Entity;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Context;

namespace OnionArcBLogProject.Service.Base
{
    using OnionArcBLogProject.Core.Entity.Enum;
    using OnionArcBLogProject.Entities.Entities;

    public class BaseService<T> : ICoreService<T> where T : CoreEntity
    {
        private readonly BlogContext _context;
        public BaseService(BlogContext blogContext)
        {
            _context = blogContext;
        }


        public bool Add(T item)
        {
            
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    _context.Set<T>().AddRange(item);
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public bool Add(List<T> items)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    _context.Set<T>().AddRange(items);
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool Any(Expression<Func<T, bool>> exp) => _context.Set<T>().Any(exp);


        public bool DeleteAll(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public List<T> GetActive() => _context.Set<T>().Where(x => x.Status == Core.Entity.Enum.Status.Active).ToList();
        public IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(x => x.Status == Core.Entity.Enum.Status.Active);
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        public List<T> GetAll() => _context.Set<T>().ToList();


        public T GetByDefault(Expression<Func<T, bool>> exp) => _context.Set<T>().FirstOrDefault(exp);


        public T GetById(int id) => _context.Set<T>().Find(id);


        public T GetById(Guid id) => _context.Set<T>().Find(id);


        public List<T> GetDefault(Expression<Func<T, bool>> exp) => _context.Set<T>().Where(exp).ToList();


        public bool Remove(T item)
        {
            item.Status = Core.Entity.Enum.Status.Deleted; return Update(item);
        }

        public bool Remove(Guid id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    T item = GetById(id);
                    item.Status = Core.Entity.Enum.Status.Deleted;
                    ts.Complete();
                    return Update(item);
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool RemoveAll(Expression<Func<T, bool>> exp)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var collection = GetDefault(exp);
                    int count = 0;
                    foreach (var item in collection)
                    {
                        item.Status = Status.Deleted;
                        bool operationResult = Update(item);
                        if (operationResult) count += 1;

                    }

                    if (collection.Count == count) ts.Complete();
                    else return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public bool Update(T item)
        {
            try
            {
                _context.Set<T>().Update(item);
                return Save() > 0;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool Activate(Guid id)
        {
            T item = GetById(id);
            item.Status = Status.Active;

            return Update(item);
        }
        public void DetachedEntity(T item)
        {
            _context.Entry<T>(item).State = EntityState.Detached; // Bir entry tkip etmeyi bırakmak için kulllanılır
        }

    }
}
