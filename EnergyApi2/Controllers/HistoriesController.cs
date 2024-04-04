using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnergyApi2.Models;
using EnergyApi2.Utils;
using System.Text.Json;
using System.Collections;
using EnergyApi2.Utils.Model;

namespace EnergyApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        private readonly EnergyTorpedaContext _context;

        public HistoriesController(EnergyTorpedaContext context)
        {
            _context = context;
        }

        // GET: api/Histories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Graph>>> GetHistories()
        {
          if (_context.Histories == null)
          {
              return NotFound();
          }
            /*var options = new JsonSerializerOptions() { WriteIndented = true };
            options.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd"));
            var histories = await _context.Histories.ToListAsync();
            List<History> list = new List<History>();
            histories.ForEach(item =>
            {
                list.Add(item);
            });*/

            //var json =  JsonSerializer.Serialize(, options);
            List<Graph> listG = new List<Graph>();
            var list = await _context.Histories.ToListAsync();
            foreach(var history in list)
            {
                if(listG.Any(item => item.date == history.DataHistory)){
                    var obj = listG.FirstOrDefault(x => x.date == history.DataHistory);
                    obj.price += history.PriceHistory;
                }
                else
                {
                    listG.Add(new Graph(history.DataHistory, history.PriceHistory));
                }
            }
            return listG;
        }

        // GET: api/Histories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<History>> GetHistory(int id)
        {
          if (_context.Histories == null)
          {
              return NotFound();
          }
            var history = await _context.Histories.FindAsync(id);

            if (history == null)
            {
                return NotFound();
            }

            return history;
        }

        // PUT: api/Histories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistory(int id, History history)
        {
            if (id != history.IdHistory)
            {
                return BadRequest();
            }

            _context.Entry(history).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryExists(id))
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

        // POST: api/Histories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<History>> PostHistory(History history)
        {
          if (_context.Histories == null)
          {
              return Problem("Entity set 'Furniture_DBContext.Histories'  is null.");
          }
            _context.Histories.Add(history);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistory", new { id = history.IdHistory }, history);
        }

        // DELETE: api/Histories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            if (_context.Histories == null)
            {
                return NotFound();
            }
            var history = await _context.Histories.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            _context.Histories.Remove(history);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoryExists(int id)
        {
            return (_context.Histories?.Any(e => e.IdHistory == id)).GetValueOrDefault();
        }
    }
}
