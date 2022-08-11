using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceApiTest.Data;
using JwtAuthntication.Model;

namespace EcommerceApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatigoriesController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public CatigoriesController(EcommerceContext context)
        {
            _context = context;
        }

        // GET: api/Catigories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Catigorie>>> GetCatigorie()
        {
          if (_context.Catigorie == null)
          {
              return NotFound();
          }
            return await _context.Catigorie.ToListAsync();
        }

        // GET: api/Catigories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Catigorie>> GetCatigorie(int id)
        {
          if (_context.Catigorie == null)
          {
              return NotFound();
          }
            var catigorie = await _context.Catigorie.FindAsync(id);

            if (catigorie == null)
            {
                return NotFound();
            }

            return catigorie;
        }

        // PUT: api/Catigories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatigorie(int id, Catigorie catigorie)
        {
            if (id != catigorie.CatigorieId)
            {
                return BadRequest();
            }

            _context.Entry(catigorie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatigorieExists(id))
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

        // POST: api/Catigories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Catigorie>> PostCatigorie(Catigorie catigorie)
        {
          if (_context.Catigorie == null)
          {
              return Problem("Entity set 'EcommerceContext.Catigorie'  is null.");
          }
            _context.Catigorie.Add(catigorie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatigorie", new { id = catigorie.CatigorieId }, catigorie);
        }

        // DELETE: api/Catigories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatigorie(int id)
        {
            if (_context.Catigorie == null)
            {
                return NotFound();
            }
            var catigorie = await _context.Catigorie.FindAsync(id);
            if (catigorie == null)
            {
                return NotFound();
            }

            _context.Catigorie.Remove(catigorie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatigorieExists(int id)
        {
            return (_context.Catigorie?.Any(e => e.CatigorieId == id)).GetValueOrDefault();
        }
    }
}
