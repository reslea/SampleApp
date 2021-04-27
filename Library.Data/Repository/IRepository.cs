using System.Collections.Generic;
using System.Linq;
using Library.Data.Entity;

namespace Library.Data.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> Get();

        T Get(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        IQueryable<T> Find();
    }
}
