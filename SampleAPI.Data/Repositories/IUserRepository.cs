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

        void Create(User user);

        void Delete(User user);
    }
}
