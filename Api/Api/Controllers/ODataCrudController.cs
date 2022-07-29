using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class ODataCrudController : ControllerBase // ODataController
    {
       // private readonly /*DbContext*/ Packt.Shared.NorthwindContext _dbctx;

        public ODataCrudController(DbContext /*Packt.Shared.NorthwindContext*/ dbctx)
        {   
            //_dbctx = dbctx;
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
