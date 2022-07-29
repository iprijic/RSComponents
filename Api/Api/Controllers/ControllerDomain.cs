using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Protocol.OData.Controller
{
    public class ControllerDomain : IControllerDomain
    {
        public ControllerDomain(IEnumerable<Type> controllerTypes)
        {
            ControllerTypes = controllerTypes;
        }

        public IEnumerable<Type> ControllerTypes { get; private set; }
    }
}
