using System;
using System.Collections.Generic;
using System.Text;

namespace Di.Tests
{
    public class UserGenerator : IUserGenerator
    {
        private readonly IGuidGenerator _guidGenerator;

        public UserGenerator(IGuidGenerator guidGenerator)
        {
            _guidGenerator = guidGenerator;
        }

        public User GetUser()
        {
            return new User
            {
                Id = _guidGenerator.Guid
            };
        }
    }

    public interface IUserGenerator
    {
        User GetUser();
    }

    public class User
    {
        public Guid Id { get; set; }

        public string Name => $"User {Id}";
    }
}
