using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Api.Query
{
    public class QueryableBuilder : IQueryableBuilder
    {
        private readonly DbContext _activeContext;
        public QueryableBuilder(DbContext activeContext)
        {
            _activeContext = activeContext;
            
        }

        public IQueryable GetRootQuery(Type clrEntityType)
        {
            IDbContextDependencies dbctxDependencies = _activeContext.GetDependencies();
            IQueryable query = ((IDbSetCache)_activeContext).GetOrAddSet(dbctxDependencies.SetSource, clrEntityType) as IQueryable;
            return query;
        }

        public IQueryable GetConstrainedQueryable(IQueryable query, String keyVal, PropertyInfo keyProperty)
        {
            ParameterExpression p = Expression.Parameter(query.ElementType, "p");
            ConstantExpression exprKeyValue = Expression.Constant(keyVal, typeof(String));
            MemberExpression exprProperty = Expression.Property(p, keyProperty);
            BinaryExpression exprEqual = Expression.Equal(Expression.Call(exprProperty, nameof(String.ToString), Type.EmptyTypes), exprKeyValue);
            LambdaExpression exprLambda = Expression.Lambda(exprEqual, p);
            return query.Provider.CreateQuery(Expression.Call(typeof(Queryable), nameof(Queryable.Where), new[] { query.ElementType }, query.Expression, exprLambda));
        }
    }
}
