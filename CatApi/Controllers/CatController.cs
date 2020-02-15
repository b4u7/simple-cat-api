using System.Linq;
using System.Threading.Tasks;
using CatApi.Data;
using CatApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CatApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatController : Controller
    {
        private readonly CatContext _context;

        public CatController(CatContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_context.Cats.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Gender,Birthday")] Cat cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            _context.Add(cat);
            await _context.SaveChangesAsync();

            return Ok(_context.Cats.OrderByDescending(o => o.Id).First());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,Birthday")] Cat cat)
        {
            if (id != cat.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(cat);
                await _context.SaveChangesAsync();

                return Ok(_context.Cats.First(o => o.Id == id));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cats.Any(o => o.Id == id))
                {
                    return NotFound();
                }

                throw;
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Cat? cat = await _context.Cats.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }
            
            return Ok(cat);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Cat? cat = await _context.Cats.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            _context.Cats.Remove(cat);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}