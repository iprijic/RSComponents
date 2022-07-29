using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.OData.Routing.Conventions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace API.Protocol.OData
{

    public interface IODataControllerActivator : IODataControllerActionConvention
    {
    }


    public interface IControllerActivatorProvider : Microsoft.AspNetCore.Mvc.Controllers.IControllerActivatorProvider
    {
        //Func<ControllerContext, object> CreateActivator(ControllerActionDescriptor descriptor);
        //Action<ControllerContext, object> CreateReleaser(ControllerActionDescriptor descriptor);
    }

}