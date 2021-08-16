using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Concrete
{
   public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        internal  DbSet<T> _entities;
        public Repository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public T Get(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<System.Linq.IQueryable<T>, System.Linq.IOrderedQueryable<T>> orderBy = null, 
            string includeProperties = null)
        {
            IQueryable<T> query = _entities;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[]{ ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            if (orderBy != null)
            {
              return  orderBy(query).ToList();
            }
            return query.ToList();
        }
        public T FirstOrDefault(Expression<Func<T, bool>> filter = null,
         string includeProperties = null)
        {
            IQueryable<T> query = _entities;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.FirstOrDefault();

        }
        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void Remove(int id)
        {
            var entity = _entities.Find(id);
            Remove(entity);
        }

      
    }
}
