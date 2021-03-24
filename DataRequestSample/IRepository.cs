using System;
using System.Collections.Generic;
using System.Text;

namespace DataRequestSample
{
    public interface IRepository<T> where T: BaseEntity
    {
        IEnumerable<T> Get();

        T Get(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}
