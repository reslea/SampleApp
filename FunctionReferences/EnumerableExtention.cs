using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp
{
    public static class EnumerableExtention
    {
        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            // if(action == null) throw new ArgumentNullException();

            foreach (var item in collection)
            {
                action?.Invoke(item);
                yield return item;
            }
        }

        public static IEnumerable<T> OurWhere<T>(this IEnumerable<T> collection, Func<T, bool> checker)
        {
            foreach (T item in collection)
            {
                if (checker(item))
                    yield return item;
            }
        }
    }
}
