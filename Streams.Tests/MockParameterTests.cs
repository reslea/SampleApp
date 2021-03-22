using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Moq;
using Xunit;

namespace Streams.Tests
{
    public class MockParameterTests
    {
        private readonly Mock<IUserStorage> _userStorageMock;

        public MockParameterTests()
        {
            _userStorageMock = new Mock<IUserStorage>();
        }

        [Fact]
        public void ParameterTest()
        {
            var userName = "Alex";
            var user = new User { Name = userName };

            _userStorageMock
                .Setup(s =>
                    s.SaveUserData(It.Is<User>(u => u.Name == userName)))
                .Returns(true);

            var authService = new AuthService(_userStorageMock.Object);

            authService.Register(user);

            _userStorageMock.Verify(s => 
                s.SaveUserData(It.IsAny<User>()), Times.Once);
        }
    }

    public class UserStorage : IUserStorage
    {
        public bool SaveUserData(User user)
        {
            Console.WriteLine($"Saved info about {user.Name}");
            return true;
        }
    }

    public interface IUserStorage
    {
        bool SaveUserData(User user);
    }

    public class AuthService : IAuthService
    {
        private readonly IUserStorage _userStorage;

        public AuthService(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public void Register(User user)
        {
            if (!_userStorage.SaveUserData(user))
            {
                throw new DataException();
            }
        }
    }

    public interface IAuthService
    {
        void Register(User user);
    }

    public class User
    {
        public string Name { get; set; }

        public string Phone { get; set; }
    }
}
