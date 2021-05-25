using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleMvc.Web.Utilities
{
    public static class LinqExtentions
    {
        // TODO: enhance with expression
        //public static IQueryable<T> Filter<T>(this IQueryable<T> collection, )

        public static IQueryable<T> CoditionalWhere<T>(this IQueryable<T> collection, bool condition,
            Expression<Func<T, bool>> expression)
        {
            return condition
                ? collection.Where(expression)
                : collection;
        }
    }
}
