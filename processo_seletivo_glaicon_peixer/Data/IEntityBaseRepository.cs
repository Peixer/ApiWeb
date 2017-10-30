using System;
using System.Collections.Generic;

namespace processo_seletivo_glaicon_peixer.Data
{
    public interface IEntityBaseRepository<T>
    {
        IEnumerable<T> GetAll();
        T Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
        T GetSingle(Guid id);
    }
}