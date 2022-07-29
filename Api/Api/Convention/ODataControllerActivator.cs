using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.OData.UriParser;
using Microsoft.OData.Edm;

using Microsoft.AspNetCore.OData.Routing.Conventions;
using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Routing;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using API.Protocol.OData.Controller;

namespace API.Protocol.OData
{

    public class ODataControllerActivator : ODataSegmentTemplate, IODataControllerActivator
    {

        public Int32 Order => 0;
        private readonly IEnumerable<Type> _predefinedControllers;
        protected IControllerDomain controllerDomain;

        public ODataControllerActivator(IControllerDomain domain)
        {
            _predefinedControllers = new Type[] { typeof(ODataCrudController) };
            controllerDomain = domain;
        }

        public Boolean AppliesToAction(ODataControllerActionContext context)
        {
            if (_predefinedControllers.Contains(context.Controller.ControllerType.UnderlyingSystemType) && (new[] { "Get", "Post", "Patch", "Delete" }.Contains(context.Action.ActionName)))
            {
                context.Action.AddSelector(context.Action.ActionName, context.Prefix, context.Model, new ODataPathTemplate(this));
                return true;
            }

            return false;
        }

        public Boolean AppliesToController(ODataControllerActionContext context)
        {
            Boolean selectedController = _predefinedControllers.Union(controllerDomain.ControllerTypes).Contains(context.Controller.ControllerType.AsType());
            return selectedController;
        }

        public override IEnumerable<String> GetTemplates(ODataRouteOptions options)
        {
            yield return "/{resource}({key})";
            yield return "/{resource}";
        }

        public override bool TryTranslate(ODataTemplateTranslateContext context)
        {
            context.RouteValues.TryGetValue("resource", out Object resource);
            String entitySetName = resource as String;

            IEdmEntitySet edmEntitySet = context.Model.EntityContainer.EntitySets()
                .FirstOrDefault(e => string.Equals(entitySetName, e.Name, StringComparison.OrdinalIgnoreCase));

            if (edmEntitySet != null)
            {
                context.Segments.Add(new EntitySetSegment(edmEntitySet));
                return true;
            }

            return false;
        }

    }

    //////////////////////////////////
    ///
    public class ControllerActivatorProvider : IControllerActivatorProvider
    {

        private static readonly Action<ControllerContext, object> _dispose = null; //Dispose;

        private readonly Func<ControllerContext, object> _controllerActivatorCreate;
        private readonly Action<ControllerContext, object> _controllerActivatorRelease;

        public ControllerActivatorProvider(IControllerActivator controllerActivator)
        {
            _controllerActivatorCreate = null;
            _controllerActivatorRelease = null;

        }

        public Func<ControllerContext, object> CreateActivator(ControllerActionDescriptor descriptor)
        {
            var controllerType = descriptor.ControllerTypeInfo?.AsType();
            if (_controllerActivatorCreate != null)
            {
                return _controllerActivatorCreate;
            }

            var typeActivator = ActivatorUtilities.CreateFactory(controllerType, Type.EmptyTypes);
            Func<ControllerContext, object> result = controllerContext =>
            {
                Object controller = typeActivator(controllerContext.HttpContext.RequestServices, arguments: null);

#if false
                if (controller is ResourceController)
                    ((ResourceController)controller).Initialize(controllerContext);
#endif

                return controller;
            };

            return result;
        }

        public Action<ControllerContext, object> CreateReleaser(ControllerActionDescriptor descriptor)
        {

            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            if (_controllerActivatorRelease != null)
            {
                return _controllerActivatorRelease;
            }

            if (typeof(IDisposable).GetTypeInfo().IsAssignableFrom(descriptor.ControllerTypeInfo))
            {
                return _dispose;
            }

            return null;
        }
    }

}