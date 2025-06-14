using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TindaTrackAPI.DTOs.Item;
using TindaTrackAPI.DTOs.Salesman;
using TindaTrackAPI.Models;

namespace TindaTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesmenController : ControllerBase
    {
        private readonly TindaTrackContext _context;

        public SalesmenController(TindaTrackContext context)
        {
            _context = context;
        }

        // GET: api/Salesmen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesmanDto>>> GetSalesmen()
        {
            var salesmen = await _context.Salesmen
            .Select(salesman => new SalesmanDto
            {
                Id = salesman.Id,
                FirstName = salesman.FirstName,
                LastName = salesman.LastName
            })
            .ToListAsync();

            return Ok(salesmen);
        }

        // GET: api/Salesmen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesmanDto>> GetSalesman(int id)
        {
            var salesman = await _context.Salesmen.FindAsync(id);

            if (salesman == null)
            {
                return NotFound();
            }

            var dto = new SalesmanDto
            {
                Id = salesman.Id,
                FirstName = salesman.FirstName,
                LastName = salesman.LastName
            };  

            return Ok(dto);
        }

        // PUT: api/Salesmen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesman(int id, CreateSalesmanDto dto)
        {
            var salesman = await _context.Salesmen.FindAsync(id);
            if (salesman == null) return NotFound();

            salesman.FirstName = dto.FirstName;
            salesman.LastName = dto.LastName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesmanExists(id))
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

        // POST: api/Salesmen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalesmanDto>> PostSalesman(CreateSalesmanDto dto)
        {
            var salesman = new Salesman
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            _context.Salesmen.Add(salesman);
            await _context.SaveChangesAsync();

            var resultDto = new SalesmanDto
            {
                Id = salesman.Id,
                FirstName = salesman.FirstName,
                LastName = salesman.LastName
            };  

            return CreatedAtAction(nameof(GetSalesman), new { id = salesman.Id }, salesman);
        }

        // DELETE: api/Salesmen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesman(int id)
        {
            var salesman = await _context.Salesmen.FindAsync(id);
            if (salesman == null)
            {
                return NotFound();
            }

            _context.Salesmen.Remove(salesman);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalesmanExists(int id)
        {
            return _context.Salesmen.Any(e => e.Id == id);
        }
    }
}
