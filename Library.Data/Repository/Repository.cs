using System.Collections.Generic;
using System.Linq;
using Library.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> DbSet;

        public Repository(LibraryContext context)
        {
            DbSet = context.Set<T>();
        } 

        public IEnumerable<T> Get()
        {
            return DbSet.ToList();
        }

        public T Get(int id)
        {
            return DbSet.FirstOrDefault(_ => _.Id == id);
        }

        public IQueryable<T> Find()
        {
            return DbSet;
        }

        public void Create(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }
    }
}
