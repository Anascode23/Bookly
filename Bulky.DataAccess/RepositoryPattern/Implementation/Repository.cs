using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Bookly.DataAccess.Repository.Interface;
using Bookly.DataAccess.Data;

namespace Bookly.DataAccess.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BooklyDbContext _context;
        internal DbSet<T> dbset;

        public Repository(BooklyDbContext context)
        {
            _context = context;
            dbset = _context.Set<T>();
            _context.Products.Include(u => u.Category);
        }

        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public void DeleteAll(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbset;
            }
            else
            {
                query = dbset.AsNoTracking();

            }

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {

                foreach (var property in includeProperties.
                    Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }

            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (!string.IsNullOrEmpty(includeProperties))
            {

                foreach (var property in includeProperties.
                    Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }

            }
            return query.ToList();
        }
    }
}
