using Borna.Application.Interfaces;
using Borna.Domain.Entities.Course;
using Borna.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Borna.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        private readonly IDataBaseContext _context;
        public CourseApiController(IDataBaseContext context)
        {
            _context = context;
        }
        // GET: api/<CourseApiController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<CourseApiController>/5
        [HttpGet("Search")]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            try
            {
                string term = HttpContext.Request.Query["term"];
                var titles = _context.Courses.Where(p => p.CourseTitle.Contains(term)).Select(p => p.CourseTitle).ToList();
                return Ok(titles);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        //// POST api/<CourseApiController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<CourseApiController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CourseApiController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
