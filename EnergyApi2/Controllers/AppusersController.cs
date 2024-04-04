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
    public class AppusersController : ControllerBase
    {
        private readonly EnergyTorpedaContext _context;

        public AppusersController(EnergyTorpedaContext context)
        {
            _context = context;
        }

        // GET: api/Appusers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appuser>>> GetAppusers()
        {
          if (_context.Appusers == null)
          {
              return NotFound();
          }
            return await _context.Appusers.ToListAsync();
        }

        // GET: api/Appusers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appuser>> GetAppuser(int id)
        {
          if (_context.Appusers == null)
          {
              return NotFound();
          }
            var appuser = await _context.Appusers.FindAsync(id);

            if (appuser == null)
            {
                return NotFound();
            }

            return appuser;
        }

        // PUT: api/Appusers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppuser(int id, Appuser appuser)
        {
            if (id != appuser.IdAppUser)
            {
                return BadRequest();
            }

            _context.Entry(appuser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppuserExists(id))
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

        // POST: api/Appusers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appuser>> PostAppuser(Appuser appuser)
        {
          if (_context.Appusers == null)
          {
              return Problem("Entity set 'Furniture_DBContext.Appusers'  is null.");
          }
            _context.Appusers.Add(appuser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppuser", new { id = appuser.IdAppUser }, appuser);
        }

        [HttpPost("{mail}/{pass}/{passNew}")]
        public async Task<ActionResult<Appuser>> PostPass(string mail, string pass, string passNew)
        {
            if (mail == null)
            {
                return NotFound();
            }
            Appuser user2 = _context.Appusers.FirstOrDefault(x => x.Mail == mail);
            if (user2 == null)
            {
                return NotFound();
            }
            if (!CryptoApp.AreEqual(pass, user2.AppPassword, user2.Salt))
            {
                return BadRequest();
            }
            user2.AppPassword = CryptoApp.GenerateHash(passNew, user2.Salt);

            _context.Entry(user2).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppuserExists(user2.IdAppUser))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user2);
        }

        // DELETE: api/Appusers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppuser(int id)
        {
            if (_context.Appusers == null)
            {
                return NotFound();
            }
            var appuser = await _context.Appusers.FindAsync(id);
            if (appuser == null)
            {
                return NotFound();
            }

            _context.Appusers.Remove(appuser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppuserExists(int id)
        {
            return (_context.Appusers?.Any(e => e.IdAppUser == id)).GetValueOrDefault();
        }
    }
}
