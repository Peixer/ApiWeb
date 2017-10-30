using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace processo_seletivo_glaicon_peixer.Data
{
    public class EntityBaseRepository<T> where T : class, IEntityBase
    {
        private readonly DataContext context;

        public EntityBaseRepository(DataContext context)
        {
            this.context = context;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>().AsEnumerable();
        }
        
        public T GetSingle(Guid guid)
        {
            return context.Set<T>().FirstOrDefault(x => x.Guid == guid);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public virtual T Add(T entity)
        {
            EntityEntry dbEntityEntry = context.Entry<T>(entity);
            var entry = context.Set<T>().Add(entity);

            context.SaveChanges();

            return entry.Entity;
        }

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = context.Update<T>(entity);
        }

        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void Commit()
        {
            context.SaveChanges();
        }
    }
}