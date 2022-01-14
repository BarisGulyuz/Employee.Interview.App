using Employee.App.Data.Intefaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Employee.App.Data.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        Context c = new Context();
        DbSet<T> _object;
        public GenericRepository()
        {
            _object = c.Set<T>();
        }

        public void Delete(T p)
        {
            var DeletedEntity = c.Entry(p);
            DeletedEntity.State = EntityState.Deleted;
            c.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public void Insert(T p)
        {
            var AddedEntity = c.Entry(p);
            AddedEntity.State = EntityState.Added;
            c.SaveChanges();
        }

        public List<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _object;
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(T ex, T p)
        {
            c.Entry(ex).CurrentValues.SetValues(p);
            c.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _object.ToList();
        }
    }
}