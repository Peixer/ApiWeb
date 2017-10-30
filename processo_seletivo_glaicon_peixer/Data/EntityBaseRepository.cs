using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public virtual T Add(T entity)
        {
            context.Entry<T>(entity);
            var entry = context.Set<T>().Add(entity);

            context.SaveChanges();

            return entry.Entity;
        }

        public virtual void Update(T entity)
        {
            context.Update<T>(entity);
            context.SaveChanges();
        }

        public virtual void Delete(Guid id)
        {
            var single = GetSingle(id);
            EntityEntry dbEntityEntry = context.Entry<T>(single);
            dbEntityEntry.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public T GetSingle(Guid id)
        {
            return context.Set<T>().Find(id);
        }
    }
}