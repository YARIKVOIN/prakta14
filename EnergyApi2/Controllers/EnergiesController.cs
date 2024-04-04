using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnergyApi2.Models;
using System.Globalization;
using System.Text.Json.Nodes;
using EnergyApi2.Utils.Model;
using EnergyApi2.Utils;
using Microsoft.AspNetCore.Cors;
using System.Reflection;
using MySqlX.XDevAPI.Common;

namespace EnergyApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergiesController : ControllerBase
    {
        private readonly EnergyTorpedaContext _context;

        public EnergiesController(EnergyTorpedaContext context)
        {
            _context = context;
        }

        [HttpPost("addd/{name}/{description}/{link}/{price}")]
        public async Task<ActionResult> PostFurnitureAAA(string name, string description, string link, int price)
        {
            if (_context.Energies == null)
            {
                return NotFound();
            }
            Image image = new Image();
            image.LinkImage = link;
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            var furniture = new Energy();
            furniture.NameEnergy = name;
            furniture.DescriptionEnergy = description;
            furniture.Price = price;
            furniture.ImageId = image.IdImage;
            furniture.CategoryId = 1;
            _context.Energies.Add(furniture);
            await _context.SaveChangesAsync();
            return Ok(furniture);
        }



        // GET: api/Furnitures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Energy>>> GetFurnitures()
        {
          if (_context.Energies == null)
          {
              return NotFound();
          }
            return await _context.Energies.ToListAsync();
        }
        // GET: api/Furnitures/sort
        [HttpGet("/sort")]
        public async Task<ActionResult<IEnumerable<Energetik>>> GetSort(string? search, string sortBy, string order, int? category, [FromQuery] PaginationFilter filter)
        {
            if (_context.Energies == null)
            {
                return NotFound();
            }
            List<Energetik> result = new List<Energetik>();
            var data = _context.Energies
                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                  .Take(filter.PageSize);
            if(sortBy == "price")
            {
                if (order == "asc")
                {
                    data = data.OrderByDescending(p => p.Price);
                }
                else
                {
                    data = data.OrderBy(p => p.Price); //Возрастанию
                }
            }
            if(sortBy == "title")
            {
                if (order == "asc")
                {
                    data = data.OrderByDescending(p => p.NameEnergy);
                }
                else
                {
                    data = data.OrderBy(p => p.NameEnergy); //Возрастанию
                }
            }
            if(search != "" && search != null)
            {
                data = data.Where(p => p.NameEnergy.Contains(search));
            }
            if(category != null)
            {
                var category_data = _context.Categories
                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                  .Take(filter.PageSize);

                var category_id = category_data.Select(t => t.IdCategory).Where(p => p == category);

                data = data.Where(p => category_id.ToList().Contains(p.CategoryId));
            }
            foreach (var item in data.ToArray())
            {
                Image image = await _context.Images.FindAsync(item.ImageId);
                string idd = item.IdEnergy.ToString();
                result.Add(ToEnergetik.ToEnergetikGo(idd, image.LinkImage, item.NameEnergy, item.Price, item.TypeF, item.DescriptionEnergy));
            }
            return result;
        }
        [HttpGet("{id}")]
        public async Task<Energetik> GetFurniture(int id)
        {
            var item = await _context.Energies.FindAsync(id);

            Image image = await _context.Images.FindAsync(item.ImageId);
            string idd = item.IdEnergy.ToString();

            return ToEnergetik.ToEnergetikGo(idd, image.LinkImage, item.NameEnergy, item.Price, item.TypeF, item.DescriptionEnergy);

        }
        // PUT: api/Furnitures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFurniture(int id, Energy energy)
        {
            if (id != energy.IdEnergy)
            {
                return BadRequest();
            }

            _context.Entry(energy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FurnitureExists(id))
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
        [HttpPut("fur/{id}")]
        public async Task<IActionResult> PutNewFurniture(int id, FurTwo furTwo)
        {
            Energy energy = await _context.Energies.FirstOrDefaultAsync(u => u.IdEnergy == id);
            if (energy == null)
            {
                return BadRequest();
            }
            energy.DescriptionEnergy = furTwo.Description;
            energy.NameEnergy = furTwo.Name;
            energy.Price = furTwo.Price;

            Image image = await _context.Images.FirstOrDefaultAsync(u => u.IdImage == energy.ImageId);
            image.LinkImage = furTwo.Image;

            _context.Update(image);
            _context.Update(energy);

            await _context.SaveChangesAsync();


            return NoContent();
        }

        // PUT: api/Furnitures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("sait")]
        public async Task<IActionResult> PutFurnitureSait(Mebel mebel)
        {
            Furniture furniture = await _context.Furnitures.FirstOrDefaultAsync(u => u.IdFurniture == int.Parse(mebel.id));
            if (null == furniture)
            {
                return BadRequest();
            }

            Furniture furnew = ToFur.ToFurGo(mebel, furniture);

            try
            {
                await _context.Furnitures.AddAsync(furnew);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FurnitureExists(furniture.IdFurniture))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        [HttpPut("sait")]
        public async Task<ActionResult<Energy>> PutFurnitureSait(int id, string title, string description, int price)
        {
            Energy furniture = await _context.Energies.FirstOrDefaultAsync(u => u.IdEnergy == id);
            if(furniture == null)
            {
                return BadRequest();
            }
            furniture.NameEnergy = title;
            furniture.DescriptionEnergy = title;
            furniture.Price = price;

            _context.Update(furniture);
            await _context.SaveChangesAsync();
            return Ok(furniture);
        }

        // POST: api/Furnitures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Energy>> PostFurniture(Energy furniture)
        {
          if (_context.Energies == null)
          {
              return Problem("Entity set 'Furniture_DBContext.Furnitures'  is null.");
          }
            _context.Energies.Add(furniture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFurniture", new { id = furniture.IdEnergy }, furniture);
        }

        [HttpPost("buy/{login}")]
        public async Task<ActionResult<Energy>> PostFurnitureBuy(Buy[] buys, string login)
        {
            Console.WriteLine(buys);
            int fullPrice = 0;
            Appuser appuser = await _context.Appusers.FirstOrDefaultAsync(u => u.Login == login);
            if (appuser == null)
            {
                return BadRequest();
            }
            foreach (Buy buy in buys)
            {
                fullPrice += buy.price;
            }
            appuser.Balance -= fullPrice;
            _context.Appusers.Update(appuser);
            History history = new History();
            history.PriceHistory = fullPrice;
            history.AppUserId = appuser.IdAppUser;
            history.BasketId = 1;
            history.DataBuy = "Покупка на сумму товара - "+fullPrice+ " купил - "+ appuser.Login;
            _context.Histories.Add(history);
            await _context.SaveChangesAsync();
            return Ok();
        }
        //[EnableCors("AllowAnyOrigin")]
        // POST: api/Furnitures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("add")]
        public async Task<ActionResult> PostFurnitureTwo(FurTwo furTwo)
        {
            if (_context.Energies == null)
            {
                return NotFound();
            }
            Image image = new Image();
            image.LinkImage = furTwo.Image;
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            var furniture = new Energy();
            furniture.NameEnergy = furTwo.Name;
            furniture.DescriptionEnergy = furTwo.Description;
            furniture.Price = furTwo.Price;
            furniture.ImageId = image.IdImage;
            furniture.CategoryId = 1;
            _context.Energies.Add(furniture);
            await _context.SaveChangesAsync();
            return Ok(furTwo);
        }

        [HttpPost("bla")]
        public async Task<ActionResult> PostBla(FurTwo furTwo)
        {
           
            return Ok(furTwo);
        }

        // DELETE: api/Furnitures/5
        [HttpGet("del/{id}")]
        public async Task<IActionResult> DeleteFurniture(int id)
        {
            if (_context.Energies == null)
            {
                return NotFound();
            }
            Energy furniture = await _context.Energies.FirstOrDefaultAsync(u => u.IdEnergy == id);
            if (furniture == null)
            {
                return NotFound();
            }

            _context.Energies.Remove(furniture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FurnitureExists(int id)
        {
            return (_context.Energies?.Any(e => e.IdEnergy == id)).GetValueOrDefault();
        }
    }
}
