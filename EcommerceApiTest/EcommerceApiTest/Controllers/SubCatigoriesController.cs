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
    public class SubCatigoriesController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public SubCatigoriesController(EcommerceContext context)
        {
            _context = context;
        }

        // GET: api/SubCatigories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCatigorie>>> GetSubCatigorie()
        {
          if (_context.SubCatigorie == null)
          {
              return NotFound();
          }
            return await _context.SubCatigorie.ToListAsync();
        }

        // GET: api/SubCatigories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCatigorie>> GetSubCatigorie(int id)
        {
          if (_context.SubCatigorie == null)
          {
              return NotFound();
          }
            var subCatigorie = await _context.SubCatigorie.FindAsync(id);

            if (subCatigorie == null)
            {
                return NotFound();
            }

            return subCatigorie;
        }

        // PUT: api/SubCatigories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCatigorie(int id, SubCatigorie subCatigorie)
        {
            if (id != subCatigorie.SubCatigorieId)
            {
                return BadRequest();
            }

            _context.Entry(subCatigorie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCatigorieExists(id))
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

        // POST: api/SubCatigories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubCatigorie>> PostSubCatigorie(SubCatigorie subCatigorie)
        {
          if (_context.SubCatigorie == null)
          {
              return Problem("Entity set 'EcommerceContext.SubCatigorie'  is null.");
          }
            _context.SubCatigorie.Add(subCatigorie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubCatigorie", new { id = subCatigorie.SubCatigorieId }, subCatigorie);
        }

        // DELETE: api/SubCatigories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCatigorie(int id)
        {
            if (_context.SubCatigorie == null)
            {
                return NotFound();
            }
            var subCatigorie = await _context.SubCatigorie.FindAsync(id);
            if (subCatigorie == null)
            {
                return NotFound();
            }

            _context.SubCatigorie.Remove(subCatigorie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubCatigorieExists(int id)
        {
            return (_context.SubCatigorie?.Any(e => e.SubCatigorieId == id)).GetValueOrDefault();
        }
    }
}
