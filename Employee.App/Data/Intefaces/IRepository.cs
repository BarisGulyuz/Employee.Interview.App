using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Employee.App.Data.Intefaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        List<T> GetAll(params Expression<Func<T, object>>[] includes);
        List<T> GetAll(Expression<Func<T, bool>> filter);
        void Insert(T p);
        T Get(Expression<Func<T, bool>> filter);
        void Update(T ex, T p);
        void Delete(T p);
    }
}