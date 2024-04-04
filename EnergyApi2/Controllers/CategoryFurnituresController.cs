using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnergyApi2.Models;

namespace EnergyApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryFurnituresController : ControllerBase
    {
        private readonly EnergyTorpedaContext _context;

        public CategoryFurnituresController(EnergyTorpedaContext context)
        {
            _context = context;
        }

        // GET: api/CategoryFurnitures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryFurnitures()
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            return await _context.Categories.ToListAsync();
        }

        // GET: api/CategoryFurnitures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryFurniture(int id)
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            var categoryFurniture = await _context.Categories.FindAsync(id);

            if (categoryFurniture == null)
            {
                return NotFound();
            }

            return categoryFurniture;
        }

        // PUT: api/CategoryFurnitures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryFurniture(int id, Category categoryFurniture)
        {
            if (id != categoryFurniture.IdCategory)
            {
                return BadRequest();
            }

            _context.Entry(categoryFurniture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryFurnitureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CategoryFurnitures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategoryFurniture(Category categoryFurniture)
        {
          if (_context.Categories == null)
          {
              return Problem("Entity set 'Furniture_DBContext.CategoryFurnitures'  is null.");
          }
            _context.Categories.Add(categoryFurniture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryFurniture", new { id = categoryFurniture.IdCategory }, categoryFurniture);
        }

        // DELETE: api/CategoryFurnitures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryFurniture(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var categoryFurniture = await _context.Categories.FindAsync(id);
            if (categoryFurniture == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoryFurniture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryFurnitureExists(int id)
        {
            return (_context.Categories?.Any(e => e.IdCategory == id)).GetValueOrDefault();
        }
    }
}
