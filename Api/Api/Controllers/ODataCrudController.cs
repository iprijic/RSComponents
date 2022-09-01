using Api.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Abstracts;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.UriParser;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class ODataCrudController : ControllerBase  /*ODataController*/
    {
        private readonly DbContext _dbctx;
        private readonly IQueryableBuilder _queryDbBuilder;


        public ODataCrudController(IHttpContextAccessor httpAccessor)
        {  
            IODataFeature odataFeature = httpAccessor.HttpContext.Request.ODataFeature();
            ODataOptions odataOptions = httpAccessor.HttpContext.ODataOptions();
            _dbctx = odataOptions.RouteComponents[odataFeature.RoutePrefix].ServiceProvider.GetService(typeof(DbContext)) as DbContext;
            _queryDbBuilder = odataOptions.RouteComponents[odataFeature.RoutePrefix].ServiceProvider.GetService<IQueryableBuilder>();
        }

        public async Task<IActionResult> Get(String resource, String key)
        {
            IQueryable query = null;
            IODataFeature odataFeature = Request.ODataFeature();
            Type clrEntityType = EntityHelper.GetClrType(odataFeature.Path, odataFeature.Model);
            query = _queryDbBuilder.GetRootQuery(clrEntityType);

            if(key != null)
            {
                query = _queryDbBuilder.GetConstrainedQueryable(query, key, EntityHelper.GetPropertyInfo(EntityHelper.GetEdmPrimaryKey(odataFeature.Path), odataFeature.Model));
            }

            await Task.CompletedTask;
            return Ok(query);
        }

        public async Task<IActionResult> Post(String resource, String key)
        {
            IQueryable query = null;
            await Task.CompletedTask;
            return Ok(query);
        }

        public async Task<IActionResult> Patch(String resource, String key)
        {
            IQueryable query = null;
            await Task.CompletedTask;
            return Ok(query);
        }

        public async Task<IActionResult> Delete(String resource, String key)
        {
            IQueryable query = null;
            await Task.CompletedTask;
            return Ok(query);
        }
    }
}
