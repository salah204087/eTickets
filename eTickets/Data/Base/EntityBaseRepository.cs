using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace eTickets.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;
        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity=_context.Set<T>().FirstOrDefault(x => x.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            var result = _context.Set<T>().ToList();
            return result;
        }

		public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
		{
            IQueryable<T> query = _context.Set<T>();
            query=includeProperties.Aggregate(query,(current,includeProperty)=>current.Include(includeProperty));
            return query.ToList();
		}

		public T GetById(int id)
        {
            var result = _context.Set<T>().FirstOrDefault(x => x.Id == id);
            return result;
        }

        public void Update(int id, T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
