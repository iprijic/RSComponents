using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Abstracts;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class ODataCrudController : ControllerBase  /*ODataController*/
    {
        private readonly DbContext _dbctx;

        public ODataCrudController(IHttpContextAccessor httpAccessor)
        {  
            IODataFeature odataFeature = httpAccessor.HttpContext.Request.ODataFeature();
            ODataOptions odataOptions = httpAccessor.HttpContext.ODataOptions();
            _dbctx = odataOptions.RouteComponents[odataFeature.RoutePrefix].ServiceProvider.GetService(typeof(DbContext)) as DbContext;
        }

        public async Task<IActionResult> Get(String resource, String key)
        {
            IQueryable query = null;
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
