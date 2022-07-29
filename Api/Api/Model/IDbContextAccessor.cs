using Microsoft.EntityFrameworkCore;

namespace Api.Model
{
    public class IDbContextAccessor
    {
        public DbContext Context { get; }
    }
}
