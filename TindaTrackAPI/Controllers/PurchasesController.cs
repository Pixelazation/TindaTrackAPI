using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TindaTrackAPI.DTOs.Purchase;
using TindaTrackAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TindaTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly TindaTrackContext _context;

        public PurchasesController(TindaTrackContext context)
        {
            _context = context;
        }

        // GET: api/Purchases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetPurchases()
        {
            var purchases = await _context.Purchases
            .Select(purchase => new PurchaseDto
            {
                Id = purchase.Id,
                OrderId = purchase.OrderId,
                ItemName = purchase.Item.Name,
                Quantity = purchase.Quantity,
                UnitPrice = purchase.UnitPrice,
                TotalAmount = purchase.TotalAmount,
            })
            .ToListAsync();

            return Ok(purchases);
        }

        // GET: api/Purchases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDto>> GetPurchase(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);

            if (purchase == null)
            {
                return NotFound();
            }

            var dto = new PurchaseDto
            {
                Id = purchase.Id,
                OrderId = purchase.OrderId,
                ItemName = purchase.Item.Name,
                Quantity = purchase.Quantity,
                UnitPrice = purchase.UnitPrice,
                TotalAmount = purchase.TotalAmount,
            };

            return Ok(dto);
        }

        // PUT: api/Purchases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchase(int id, CreatePurchaseDto dto)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null) return NotFound();

            purchase.OrderId = dto.OrderId;
            purchase.ItemId = dto.ItemId;
            purchase.Quantity = dto.Quantity;
            purchase.UnitPrice = dto.UnitPrice;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: api/Purchases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurchaseDto>> PostPurchase(CreatePurchaseDto dto)
        {
            var purchase = new Purchase
            {
                OrderId = dto.OrderId,
                ItemId = dto.ItemId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
            };

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            var resultDto = new PurchaseDto
            {
                Id = purchase.Id,
                OrderId = purchase.OrderId,
                ItemName = purchase.Item.Name,
                Quantity = purchase.Quantity,
                UnitPrice = purchase.UnitPrice,
            };

            return CreatedAtAction(nameof(GetPurchase), new { id = purchase.Id }, resultDto);
        }

        // DELETE: api/Purchases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchases.Any(e => e.Id == id);
        }
    }
}
