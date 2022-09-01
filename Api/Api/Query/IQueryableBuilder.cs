using System.Linq;
using System.Reflection;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Query;

namespace Api.Query
{
    public interface IQueryableBuilder
    {
        public IQueryable GetConstrainedQueryable(IQueryable query, String keyVal, PropertyInfo keyProperty);
        public IQueryable GetRootQuery(Type clrEntityType);
        public IQueryable ApplyODataQueryAsync(IQueryable query, ODataQueryOptions option);
    }
}
