using System;
using Di.Container;
using Xunit;

namespace Di.Tests
{
    public class DiTests
    {
        [Fact]
        public void Container_Returns_Object()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<GuidGenerator>();

            var container = serviceCollection.GenerateContainer();

            var generator = container.Get<GuidGenerator>();

            Assert.NotNull(generator);
            Assert.NotEqual(new Guid(), generator.Guid);
        }

        [Fact]
        public void Transient_Returns_New_Object_Each_Time()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<GuidGenerator>();

            var container = serviceCollection.GenerateContainer();

            var generator1 = container.Get<GuidGenerator>();
            var generator2 = container.Get<GuidGenerator>();

            Assert.NotEqual(generator1.Guid, generator2.Guid);
        }

        [Fact]
        public void Singleton_Returns_Same_Object_Each_time()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<GuidGenerator>();

            var container = serviceCollection.GenerateContainer();

            var generator1 = container.Get<GuidGenerator>();
            var generator2 = container.Get<GuidGenerator>();

            Assert.Equal(generator1.Guid, generator2.Guid);
        }

        [Fact]
        public void Transient_By_Interface_Returns_Object()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IGuidGenerator, GuidGenerator>();

            var container = serviceCollection.GenerateContainer();

            var generator1 = container.Get<IGuidGenerator>();
            var generator2 = container.Get<IGuidGenerator>();

            Assert.NotEqual(generator1.Guid, generator2.Guid);
        }

        [Fact]
        public void Singleton_By_Interface_Reuses_Object()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IGuidGenerator, GuidGenerator>();

            var container = serviceCollection.GenerateContainer();

            var generator1 = container.Get<IGuidGenerator>();
            var generator2 = container.Get<IGuidGenerator>();

            Assert.Equal(generator1.Guid, generator2.Guid);
        }

        [Fact]
        public void Container_Resolves_Object_With_Dependencies()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IGuidGenerator, GuidGenerator>();
            serviceCollection.AddTransient<IUserGenerator, UserGenerator>();

            var container = serviceCollection.GenerateContainer();

            var userGenerator = container.Get<IUserGenerator>();

            Assert.NotNull(userGenerator);

            var user = userGenerator.GetUser();

            Assert.NotEqual(new Guid(), user.Id);
        }

        [Fact]
        public void Container_Resolves_Object_With_Dependencies_And_Lifetime()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IGuidGenerator, GuidGenerator>();
            serviceCollection.AddTransient<IUserGenerator, UserGenerator>();

            var container = serviceCollection.GenerateContainer();

            var userGenerator1 = container.Get<IUserGenerator>();
            var userGenerator2 = container.Get<IUserGenerator>();

            Assert.Equal(
                userGenerator1.GetUser().Id,
                userGenerator2.GetUser().Id);
        }
    }
}
