using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Di.Container
{
    public class DiContainer
    {
        private readonly List<ServiceDescriptor> _serviceDescriptors;

        public DiContainer(List<ServiceDescriptor> serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors;
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        private object Get(Type serviceType)
        {
            var descriptor = _serviceDescriptors
                .SingleOrDefault(d => d.Abstraction == serviceType);

            if (descriptor == null)
            {
                throw new ArgumentException($"cannot create {serviceType.Name}");
            }

            if (descriptor.LifetimeType == LifetimeType.Singleton)
            {
                descriptor.Object ??= CreateService(descriptor.Implementation);

                return descriptor.Object;
            }

            return CreateService(descriptor.Implementation);
        }

        private object CreateService(Type serviceType)
        {
            var constructorParameterInfos = serviceType
                .GetConstructors()
                .First()
                .GetParameters();

            if (constructorParameterInfos.Length == 0)
            {
                return Activator.CreateInstance(serviceType);
            }

            var constructorParameters = new List<object>();

            //var constructorParameters = constructorParameterInfos
            //    .Select(p => Get(p.ParameterType))
            //    .ToArray();

            foreach (var param in constructorParameterInfos)
            {
                var paramType = param.ParameterType;
                var createdParam = Get(paramType);
                constructorParameters.Add(createdParam);
            }

            return Activator.CreateInstance(serviceType, constructorParameters.ToArray());
;        }
    }
}
