using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Context;

namespace OnionArcBLogProject.Service.Base
{
    using OnionArcBLogProject.Entities.Entities;
    using OnionArcBLogProject.Entities.Enum;

    public class BaseService<T> : ICoreService<T> where T : CoreEntity
    {
        private readonly BlogContext _context;
        private readonly DbSet<T> _db;
        public BaseService(BlogContext blogContext)
        {
            _context = blogContext;
            _db = _context.Set<T>();
        }


        public bool Add(T item)
        {
            
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    _db.AddRange(item);
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
                   _db.AddRange(items);
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool Any(Expression<Func<T, bool>> exp) => _db.Any(exp);


        public bool DeleteAll(Expression<Func<T, bool>> exp)
        {
            _db.Where(exp);
            return false;
        }

        public List<T> GetActive() => _db.Where(x => x.Status == Status.Active).ToList();
        public IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes)
        {
            var query = _db.Where(x => x.Status == Status.Active);
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        public List<T> GetAll() => _db.ToList();


        public T GetByDefault(Expression<Func<T, bool>> exp) => _db.FirstOrDefault(exp);


        public T GetById(int id) => _db.Find(id);


        public T GetById(Guid id) => _db.Find(id);


        public List<T> GetDefault(Expression<Func<T, bool>> exp) => _db.Where(exp).ToList();


        public bool Remove(T item)
        {
            item.Status = Status.Deleted; return Update(item);
        }

        public bool Remove(Guid id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    T item = GetById(id);
                    item.Status = Status.Deleted;
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
                _db.Update(item);
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

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = _db.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }
    }
}
