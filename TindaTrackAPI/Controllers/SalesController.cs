using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TindaTrackAPI.DTOs.Sale;
using TindaTrackAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TindaTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly TindaTrackContext _context;

        public SalesController(TindaTrackContext context)
        {
            _context = context;
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSales()
        {
            var sales = await _context.Sales
            .Select(sale => new SaleDto
            {
                Id = sale.Id,
                Date = sale.Date,
                Quantity = sale.Quantity,
                UnitPrice = sale.UnitPrice,
                TotalAmount = sale.TotalAmount,
                BarangayName = sale.Barangay.Name,
                MunicipalityName = sale.Barangay.Municipality.Name,
                SalesmanName = sale.Salesman.LastName + ", " + sale.Salesman.FirstName,
                ItemName = sale.Item.Name
            })
            .ToListAsync();

            return Ok(sales);
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            var dto = new SaleDto
            {
                Id = sale.Id,
                Date = sale.Date,
                Quantity = sale.Quantity,
                UnitPrice = sale.UnitPrice,
                TotalAmount = sale.TotalAmount,
                BarangayName = sale.Barangay.Name,
                MunicipalityName = sale.Barangay.Municipality.Name,
                SalesmanName = sale.Salesman.LastName + ", " + sale.Salesman.FirstName,
                ItemName = sale.Item.Name
            };

            return Ok(dto);
        }

        // PUT: api/Sales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, CreateSaleDto dto)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return NotFound();

            sale.Date = dto.Date;
            sale.Quantity = dto.Quantity;
            sale.UnitPrice = dto.UnitPrice;
            sale.BarangayId = dto.BarangayId;
            sale.SalesmanId = dto.SalesmanId;
            sale.ItemId = dto.ItemId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
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

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SaleDto>> PostSale(CreateSaleDto dto)
        {
            var sale = new Sale
            {
                Date = dto.Date,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                BarangayId = dto.BarangayId,
                SalesmanId = dto.SalesmanId,
                ItemId = dto.ItemId,
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            var resultDto = new SaleDto
            {
                Id = sale.Id,
                Date = sale.Date,
                Quantity = sale.Quantity,
                UnitPrice = sale.UnitPrice,
                TotalAmount = sale.TotalAmount,
                BarangayName = sale.Barangay.Name,
                MunicipalityName = sale.Barangay.Municipality.Name,
                SalesmanName = sale.Salesman.LastName + ", " + sale.Salesman.FirstName,
                ItemName = sale.Item.Name
            };

            return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, resultDto);
        }

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
