using System;
using System.Collections.Generic;
using System.Text;
using SampleAPI.Data.Entities;

namespace SampleAPI.Data.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> Get();

        User Get(int id);

        User Get(string email, string password);

        void Create(User user);

        void Delete(User user);

        void Update(User user);
    }
}
