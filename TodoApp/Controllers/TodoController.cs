using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Repositories.Interfaces;
using TodoApp.Services.IServices;

namespace TodoApp.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] for apply all action in controller
    [Route("api/[controller]")] // mean api/todo
    [ApiController]
    public class TodoController : ControllerBase
    {
        //private readonly ApiDbContext _context;
        private readonly ITodoRepository _todoRepos;
        private readonly ICaculator _caculator;

        public TodoController(ITodoRepository todoRepository, ICaculator caculator)
        {
            _todoRepos = todoRepository;
            _caculator = caculator;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = _caculator.Add(100, 200);
            var items = await _todoRepos.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _todoRepos.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemData data)
        {
            if (ModelState.IsValid)
            {
                await _todoRepos.Create(data);

                return CreatedAtAction("GetItem", new { data.Id }, data);
            }

          //      return new JsonResult("Something Went wrong") { StatusCode = 400 };
            return BadRequest("Something went wrong");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, ItemData item)
        {
            // check if the id of formdata not equal ItemData Id attribute
            if (id != item.Id)
                return BadRequest();

            var existItem = await _todoRepos.GetById(id);

            if (existItem == null)
                return NotFound();

            existItem.Title = item.Title;
            existItem.Details = item.Details;

            await _todoRepos.Save();

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var existItem = await _todoRepos.GetById(id);

            if (existItem == null)
                return NotFound();


            await _todoRepos.Delete(existItem);

            return Ok(existItem);
        }
    }
}
