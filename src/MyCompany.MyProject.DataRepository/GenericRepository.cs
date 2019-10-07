using DataBase;
using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.DataRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyCompany.MyProject.DataRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private MyProjectContext _context;
        private DbSet<TEntity> _dbSet;
        public GenericRepository(MyProjectContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public void Delete(object Id)
        {
            TEntity DeleteItem = _dbSet.Find(Id);
            Delete(DeleteItem);
        }

        public void Delete(TEntity DeleteItem)
        {
            if (_context.Entry(DeleteItem).State == EntityState.Deleted)
            {
                _dbSet.Attach(DeleteItem);
            }
            _dbSet.Remove(DeleteItem);
        }

        public IQueryable<TEntity> Entity()
        {
            return _dbSet;
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, object>>[] includeProperties = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    query = query.Include(property);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity GetById(object Id)
        {
            return _dbSet.Find(Id);
        }

        public void Insert(TEntity NewItem)
        {
            _dbSet.Add(NewItem);

        }

        public void Update(TEntity UpdateItem)
        {
            _dbSet.Attach(UpdateItem);
            _context.Entry(UpdateItem).State = EntityState.Modified;
        }
    }
}
