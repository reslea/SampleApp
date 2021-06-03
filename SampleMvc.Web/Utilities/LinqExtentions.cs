using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using SampleMvc.Web.Models;

namespace SampleMvc.Web.Utilities
{
    public static class LinqExtentions
    {
        public static IQueryable<T> Filter<T, TKey>(this IQueryable<T> collection,
            Expression<Func<object, TKey>> keyExpression,
            Expression<Func<T, bool>> expression)
        {
            if (!(keyExpression.Body is MemberExpression memberExpression) ||
                !(memberExpression.Member is PropertyInfo propertyInfo))
            {
                throw new ArgumentException("Its not a property expression");
            }

            var getter = propertyInfo.GetMethod;

            // TODO: use real model (not this fake one)
            var obj = new FilterModel()
            {
                Author = "check"
            };
            var isKeyNotNull = getter
                ?.Invoke(obj, new object?[] { }) != null;
                
            return isKeyNotNull
                ? collection.Where(expression)
                : collection;
        }

        public static IQueryable<T> CoditionalWhere<T>(this IQueryable<T> collection, bool condition,
            Expression<Func<T, bool>> expression)
        {
            return condition
                ? collection.Where(expression)
                : collection;
        }
    }
}
