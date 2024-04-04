
using EnergyApi2;
using EnergyApi2.Models;
using EnergyApi2.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EnergyApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegController : ControllerBase
    {
        LogWriter logWriter = new LogWriter();
        private readonly EnergyTorpedaContext _context;

        public RegController(EnergyTorpedaContext context)
        {
            _context = context;
        }

        // POST api/users
        [HttpPost("{pass}/{mail}")]
        public async Task<ActionResult<Appuser>> Post(string pass, string mail)
        {
            if ( pass == null || mail == null)
            {
                return BadRequest();
            }
            if (_context.Appusers.Any(x => x.Mail == mail))
            {
                return NotFound();
            }
            Appuser appuser = new Appuser();
            appuser.Salt = CryptoApp.CreateSalt();
            appuser.AppPassword = CryptoApp.GenerateHash(pass, appuser.Salt);
            appuser.Balance = 10000;
            appuser.RolesId = 1;
            appuser.Status = true;
            appuser.Login = mail;
            appuser.Mail = mail;
            _context.Appusers.Add(appuser);
            await _context.SaveChangesAsync();
            logWriter.LogWrite("Пользователь: " + mail + " зарегестрировался");
            return Ok(appuser);
        }
    }
}