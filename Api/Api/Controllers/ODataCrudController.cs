using Api.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Abstracts;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.UriParser;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class ODataCrudController : ControllerBase
    {
        private DbContext _dbctx;
        private IQueryableBuilder _queryDbBuilder;


        public ODataCrudController()
        {

        }

        private Boolean InitFromFeature()
        {
            IODataFeature odataFeature = Request.ODataFeature();
            if (odataFeature == null)
                return false;
            ODataOptions odataOptions = Request.HttpContext.ODataOptions();
            _dbctx = odataOptions.RouteComponents[odataFeature.RoutePrefix].ServiceProvider.GetService(typeof(DbContext)) as DbContext;
            _queryDbBuilder = odataOptions.RouteComponents[odataFeature.RoutePrefix].ServiceProvider.GetService<IQueryableBuilder>();
            return true;
        }

        public async Task<IActionResult> Get(String resource, String key)
        {
            IQueryable query = null;
            if (InitFromFeature() == false)
                return BadRequest();
            IODataFeature odataFeature = Request.ODataFeature();
            Type clrEntityType = EntityHelper.GetClrType(odataFeature.Path, odataFeature.Model);
            ODataQueryOptions option = new ODataQueryOptions(new ODataQueryContext(odataFeature.Model, clrEntityType, odataFeature.Path), Request);
            query = _queryDbBuilder.GetRootQuery(clrEntityType);

            if(key != null)
            {
                query = _queryDbBuilder.GetConstrainedQueryable(query, key, EntityHelper.GetPropertyInfo(EntityHelper.GetEdmPrimaryKey(odataFeature.Path), odataFeature.Model));
            }

            if (option.SelectExpand != null)
            {
                query = _queryDbBuilder.ApplyODataQueryAsync(query, option);
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
