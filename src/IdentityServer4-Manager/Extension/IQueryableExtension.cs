using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Extension
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> Paged<T>(this IQueryable<T> source, Expression<Func<T, bool>> whereLambda,
            string orderbyStr, int pageIndex, int pageSize, bool isAsc, ref int totalCount)
        {
            totalCount = source.Where(whereLambda).Count();
            IQueryable<T> result;
            try
            {
                if (isAsc)
                {
                    result =
                     source
                    .Where(whereLambda)
                    .OrderBy(orderbyStr)
                    .Skip(pageIndex)
                    .Take(pageSize)
                    ;
                }
                else
                {
                    result =
                     source
                    .Where(whereLambda)
                    .OrderByDescending(orderbyStr)
                    .Skip(pageIndex)
                    .Take(pageSize)
                    ;
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return result;
        }

        public static IQueryable<T> Paged<T>(this DbSet<T> source, Expression<Func<T, bool>> whereLambda,
            string orderbyStr, int pageIndex, int pageSize, bool isAsc, ref int totalCount)
            where T : class
        {
            totalCount = source.Where(whereLambda).Count();
            IQueryable<T> result;
            try
            {
                if (isAsc)
                {
                    result =
                     source
                    .OrderBy(orderbyStr)
                    .Where(whereLambda)
                    .Skip(pageIndex)
                    .Take(pageSize)
                    ;
                }
                else
                {
                    result =
                     source
                    .Where(whereLambda)
                    .OrderByDescending(orderbyStr)
                    .Skip(pageIndex)
                    .Take(pageSize)
                    ;
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return result;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, params object[] values)
        {
            var type = typeof(T);
            var property = type.GetProperty(ordering);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string ordering, params object[] values)
        {

            var type = typeof(T);
            var property = type.GetProperty(ordering);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}
