using APIngay15thang7.Data;
using APIngay15thang7.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIngay15thang7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        public TodoController(TodoContext context)
        {
            _context = context;
        }
        private static List<TodoList> todos = new List<TodoList> { 
            new TodoList{Id = 1, Content = "dingu"}, 
            new TodoList{Id = 2, Content = "dingu2"}, 
            new TodoList{Id = 4, Content = "dingu3"}, 
            };
        [HttpGet]
        public async Task<ActionResult<List<TodoList>>> GetAll()
        {
           return Ok(await _context.TodoLists.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var todo = await _context.TodoLists.Where(x => x.Id == id).FirstAsync();
            if(todo == null)
                return NotFound();
            return Ok(todo);
        }
        [HttpPost]
        public async Task<ActionResult<List<TodoList>>> Add([FromForm]TodoList item)
        {
            _context.TodoLists.Add(item);
            await _context.SaveChangesAsync();
            return Ok(await _context.TodoLists.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult> Update(TodoList request)
        {
            var todo = await _context.TodoLists.FindAsync(request.Id);
            if (todo == null)
            {
                return BadRequest();
            }
            todo.Content = request.Content;
            todo.Id = request.Id;
            _context.TodoLists.Update(todo);
            await _context.SaveChangesAsync();
            return Ok(await _context.TodoLists.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var todo = await _context.TodoLists.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(todo == null)
            {
                return BadRequest();
            }
            _context.TodoLists.Remove(todo);
            await _context.SaveChangesAsync();
            return Ok(await _context.TodoLists.ToListAsync());
            
        }



    }
}
