using Shared.Constants;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Store.DataAcess.Extensions
{
    public static class OrderByFieldExtension
    {
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, bool Ascending)
        {
            var param = Expression.Parameter(typeof(T), DefaultValues.SortValue);
            var prop = Expression.Property(param, SortField);
            var exp = Expression.Lambda(prop, param);
            string method = Ascending ? DefaultValues.OrderBy : DefaultValues.OrderByDescending;
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }
    }
}
