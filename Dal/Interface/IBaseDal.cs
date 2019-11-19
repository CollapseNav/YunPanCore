using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Model;

namespace Dal.Interface
{
    public interface IBaseDal<T> where T : BaseEntity
    {

        void Add(T entity);

        void AddRange(List<T> entityList);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> exp);

        void DeleteByID(string id);

        void Update(T entity);

        void Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);

        T FindSingle(Expression<Func<T, bool>> exp = null);

        T FindByID(string id);

        IQueryable<T> FindAll(Expression<Func<T, bool>> exp = null);

        IQueryable<T> FindPage<TKey>(out int total, int pageindex = 1, int pageSize = 15, bool isAsc = true, Expression<Func<T, bool>> exp = null, Expression<Func<T, TKey>> orderBy = null);

        bool IsExist(Expression<Func<T, bool>> exp);

        int Count(Expression<Func<T, bool>> exp = null);

        void Save();

        int ExeSql(string sql);

        YunPanContext GetContext();

    }
}
