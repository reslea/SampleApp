using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Data.Entities;

namespace SampleAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _set;

        public UserRepository(SocialNetworkDbContext context)
        {
            _set = context.Set<User>();
        }

        public IEnumerable<User> Get()
        {
            return _set;
        }

        public User Get(int id)
        {
            return _set.FirstOrDefault(u => u.Id == id);
        }

        public User Get(string email, string password)
        {
            return _set.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public void Create(User user)
        {
            _set.Add(user);
        }

        public void Delete(User user)
        {
            _set.Remove(user);
        }

        public void Update(User user)
        {
            _set.Update(user);
        }
    }
}
