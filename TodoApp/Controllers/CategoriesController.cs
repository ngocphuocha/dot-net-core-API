using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")] // api/todo
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ApiDbContext _context;

        public CategoriesController(ApiDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult GetCategories()
        {
            var items = _context.Categories.ToList();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(z => z.Id == id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCategory", new { category.Id }, category);
            }

            return new JsonResult("Something Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Category category)
        {
            // check if the id of formdata not equal ItemData Id attribute
            if (id != category.Id)
                return BadRequest();

            var existItem = await _context.Categories.FirstOrDefaultAsync(z => z.Id == id);

            if (existItem == null)
                return NotFound();

            existItem.Name = category.Name;
            existItem.Description = category.Description;

            await _context.SaveChangesAsync();

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existCategory = await _context.Categories.FirstOrDefaultAsync(z => z.Id == id);

            if (existCategory == null)
            {
                return BadRequest();
            }

            _context.Remove(existCategory);
            await _context.SaveChangesAsync();

            return Ok(existCategory);
        }
    }
}
