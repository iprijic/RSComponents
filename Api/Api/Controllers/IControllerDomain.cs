using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Protocol.OData.Controller
{
    public interface IControllerDomain
    {
        public IEnumerable<Type> ControllerTypes { get; }
    }
}
