using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TindaTrackAPI.DTOs.Item;
using TindaTrackAPI.Models;
using TindaTrackAPI.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TindaTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly TindaTrackContext _context;

        public ItemsController(TindaTrackContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems
        (
            [FromQuery] int? page,
            [FromQuery] int? pageSize,
            [FromQuery] string? searchQuery,
            [FromQuery] string? filter
        )
        {
            var items = _context.Items
            .Select(item => new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                ItemCode = item.ItemCode,
                Description = item.Description,
                UnitPrice = item.UnitPrice
            });

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    filter = StringUtils.ToPascalCase(filter);
                    items = items.Where($"{filter}.Contains(@0)", searchQuery);
                }
                else
                {
                    string lowerSearch = searchQuery.ToLower();

                    items = items.Where(item =>
                        item.Name.ToLower().Contains(lowerSearch) ||
                        item.ItemCode.ToLower().Contains(lowerSearch) ||
                        item.Description != null && item.Description.ToLower().Contains(lowerSearch)
                    );
                }  
            }

            if (page != null && pageSize != null)
            {
                var position = (page.Value - 1) * pageSize.Value;
                items = items.Skip(position).Take(pageSize.Value);
            }

            var result = await items.ToListAsync();

            return Ok(result);
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null) return NotFound();

            var dto = new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                ItemCode = item.ItemCode,
                UnitPrice = item.UnitPrice
            };

            return Ok(dto);
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, CreateItemDto dto)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            item.Name = dto.Name;
            item.UnitPrice = dto.UnitPrice;
            item.ItemCode = dto.ItemCode;
            item.Description = dto.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItem(CreateItemDto dto)
        {
            if (await ItemCodeExists(dto.ItemCode))
            {
                return Conflict(new { message = "ItemCode already exists." });
            }

            var item = new Item
            {
                Name = dto.Name,
                UnitPrice = dto.UnitPrice,
                ItemCode = dto.ItemCode,
                Description = dto.Description,
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            var resultDto = new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                UnitPrice = item.UnitPrice,
                ItemCode = item.ItemCode,
                Description = item.Description,
            };

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, resultDto);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }

        private async Task<bool> ItemCodeExists(string itemCode)
        {
            return await _context.Items.AnyAsync(i => i.ItemCode == itemCode);
        }
    }
}
