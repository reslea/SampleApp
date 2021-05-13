using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleAPI.Data;

namespace SampleApi.Web.Utilities
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialNetworkDbContext _context;

        public UnitOfWork(SocialNetworkDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}
