using System.Linq;
using System.Reflection;
using System;
using System.Threading.Tasks;

namespace Api.Query
{
    public interface IQueryableBuilder
    {
        public IQueryable GetConstrainedQueryable(IQueryable query, String keyVal, PropertyInfo keyProperty);
        public IQueryable GetRootQuery(Type clrEntityType);
    }
}
