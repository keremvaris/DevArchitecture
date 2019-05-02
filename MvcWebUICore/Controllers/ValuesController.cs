using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IProductService _productService;

        public ValuesController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            //_productService.Add(new Entities.Concrete.Product());
            return _productService.GetList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
