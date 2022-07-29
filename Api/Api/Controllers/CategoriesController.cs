﻿using Microsoft.AspNetCore.Mvc; // IActionResult
using Microsoft.AspNetCore.OData.Query; // [EnableQuery]
using Microsoft.AspNetCore.OData.Routing.Controllers; // ODataController
using Packt.Shared; // NorthwindContext

namespace Northwind.OData.Controllers
{
    public class CategoryController : ODataController
    {
        private readonly NorthwindContext db;

        public CategoryController(NorthwindContext db)
        {
            this.db = db;
        }
    }

    public class CategoriesController : ODataController
    {
        private readonly NorthwindContext db;

        public CategoriesController(NorthwindContext db)
        {
            this.db = db;
        }

        //[EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Categories);
        }

        //[EnableQuery]
        //[HttpGet]
        public IActionResult Get(int key)
        {
            return Ok(db.Categories.Find(key));
        }
    }
}
