using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dal.Interface;
using Microsoft.EntityFrameworkCore;
using Model;
using Z.EntityFramework.Plus;

namespace Dal
{
    public class BaseDal<T> : IBaseDal<T> where T : BaseEntity
    {

        //上下文
        private readonly YunPanContext _db;

        /// <summary>
        /// 初始化上下文
        /// </summary>
        public BaseDal(YunPanContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 添加数据(单个)
        /// </summary>
        /// <param name="entity">新的数据</param>
        public void Add(T entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.CreateDate = DateTime.Now;
                entity.ChangedDate = DateTime.Now;
                entity.IsDeleted = 0;
            }

            _db.Set<T>().Add(entity);
            Save();
            _db.Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// 添加数据(集合)
        /// </summary>
        /// <param name="entityList">新的数据集合</param>
        public void AddRange(List<T> entityList)
        {
            foreach (var entity in entityList)
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.CreateDate = DateTime.Now;
                entity.ChangedDate = DateTime.Now;
            }
            _db.Set<T>().AddRange(entityList);
            Save();
        }

        /// <summary>
        /// 计算符合条件的数据数量
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> exp = null)
        {
            return FindAll(exp).Count();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">需要删除的数据</param>
        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
            Save();
        }

        /// <summary>
        /// 有条件地删除数据
        /// </summary>
        /// <param name="exp">筛选条件</param>
        public void Delete(Expression<Func<T, bool>> exp)
        {
            _db.Set<T>().Where(exp).Delete();
        }

        /// <summary>
        /// 根据id删除数据
        /// </summary>
        /// <param name="id">主键ID</param>
        public void DeleteByID(string id)
        {
            var entry = FindByID(id);
            _db.Entry(entry).State = EntityState.Deleted;
            Save();
        }

        /// <summary>
        /// 运行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExeSql(string sql)
        {
            return _db.Database.ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// 查询所有符合条件的数据
        /// </summary>
        /// <param name="exp">筛选条件
        /// PS:若使用默认的NULL，则返回所有数据
        /// </param>
        /// <returns></returns>
        public IQueryable<T> FindAll(Expression<Func<T, bool>> exp = null)
        {
            var dbSet = _db.Set<T>().AsNoTracking().AsQueryable();
            if (exp != null)
                dbSet = dbSet.Where(exp);
            return dbSet;
        }

        /// <summary>
        /// 根据id查询数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public T FindByID(string id)
        {
            return FindAll(m => m.Id == id).First();
        }

        /// <summary>
        /// 分页查找所有符合条件的数据
        /// </summary>
        /// <param name="isAsc">是否正序</param>
        /// <param name="exp">筛选条件</param>
        /// <param name="pageindex">分页页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderBy">排序规则</param>
        /// /// <returns></returns>
        public IQueryable<T> FindPage<TKey>(out int total, int pageindex = 1, int pageSize = 15, bool isAsc = true, Expression<Func<T, bool>> exp = null, Expression<Func<T, TKey>> orderBy = null)
        {
            total = Count(exp);
            pageindex = pageindex > 0 ? pageindex : 1;
            if (isAsc)
            {
                if (orderBy == null)
                    return FindAll(exp).OrderBy(m => m.Id).Skip(pageindex * (pageSize - 1)).Take(pageSize);
                return FindAll(exp).OrderBy(orderBy).Skip(pageindex * (pageSize - 1)).Take(pageSize);
            }

            if (orderBy == null)
            {
                return FindAll().OrderByDescending(m => m.Id).Skip(pageindex * (pageSize - 1)).Take(pageSize);
            }
            return FindAll(exp).OrderByDescending(orderBy).Skip(pageindex * (pageSize - 1)).Take(pageSize);
        }

        /// <summary>
        /// 查询单个数据
        /// </summary>
        /// <param name="exp">筛选条件</param>
        /// <returns></returns>
        public T FindSingle(Expression<Func<T, bool>> exp = null)
        {
            var entitys = FindAll(exp).ToList();
            T entity = entitys.Count() == 0 ? null : entitys.First();
            return entity;
        }

        public YunPanContext GetContext() => _db;

        /// <summary>
        /// 判断是否有符合条件的数据
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public bool IsExist(Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().FirstOrDefault(exp) != null;
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        public void Save()
        {
            _db.SaveChanges();
        }

        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="entity">The entity.</param>
        public void Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            _db.Set<T>().Where(where).Update(entity);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            var entry = _db.Entry(entity);
            entry.State = EntityState.Modified;

            //如果数据没有发生变化
            if (!_db.ChangeTracker.HasChanges())
            {
                return;
            }
            Save();
        }
    }
}
