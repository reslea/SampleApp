using System;
using System.Collections.Generic;
using System.Text;
using Library.Data.Repository;

namespace Library.Data
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>(); 

        private readonly Dictionary<Type, Type> _servicesToResolve = new Dictionary<Type, Type>();

        public ServiceLocator()
        {
            _services.Add(typeof(LibraryContext), new LibraryContext());

            _services.Add(typeof(IBookRepository), new BookRepository(Get<LibraryContext>()));
            _services.Add(typeof(IBookPriceRepository), new BookPriceRepository(Get<LibraryContext>()));
        }

        public T Get<T>()
        {
            return (T)_services[typeof(T)];
        }
    }
}
