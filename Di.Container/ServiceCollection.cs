using System;
using System.Collections.Generic;
using System.Text;

namespace Di.Container
{
    public class ServiceCollection
    {
        private readonly List<ServiceDescriptor> _serviceDescriptors
            = new List<ServiceDescriptor>();

        public DiContainer GenerateContainer()
        {
            return new DiContainer(_serviceDescriptors);
        }

        public void AddTransient<T>()
        {
            _serviceDescriptors.Add(new ServiceDescriptor(
                typeof(T), 
                typeof(T),
                LifetimeType.Transient));
        }

        public void AddTransient<TAbstraction, TImplementation>()
            where TImplementation : TAbstraction
        {
            _serviceDescriptors.Add(new ServiceDescriptor(
                typeof(TAbstraction), 
                typeof(TImplementation), 
                LifetimeType.Transient));
        }

        public void AddSingleton<TAbstraction, TImplementation>()
            where TImplementation : TAbstraction
        {
            _serviceDescriptors.Add(new ServiceDescriptor(
                typeof(TAbstraction),
                typeof(TImplementation),
                LifetimeType.Singleton));
        }

        public void AddSingleton<T>()
        {
            _serviceDescriptors.Add(new ServiceDescriptor(
                typeof(T), 
                typeof(T),
                LifetimeType.Singleton));
        }
    }

    public class ServiceDescriptor
    {
        public Type Abstraction { get; set; }

        public Type Implementation { get; set; }

        public object Object { get; set; }

        public LifetimeType LifetimeType { get; set; }

        public ServiceDescriptor(Type abstraction, Type implementation, LifetimeType lifetimeType)
        {
            Implementation = implementation;
            Abstraction = abstraction;
            LifetimeType = lifetimeType;
        }
    }

    public enum LifetimeType
    {
        Transient,
        Singleton
    }
}
