using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using TindaTrackAPI.DTOs.Order;
using TindaTrackAPI.Models;

namespace TindaTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly TindaTrackContext _context;

        public OrdersController(TindaTrackContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _context.Orders
            .Select(order => new OrderDto
            {
                Id = order.Id,
                AccountName = order.Account.Name,
                SalesmanName = $"{order.Salesman.FirstName} {order.Salesman.LastName}",
                Date = order.Date,
                TotalSales = order.TotalSales,
            })
            .ToListAsync();

            return Ok(orders);
        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var dto = new OrderDto
            {
                Id = order.Id,
                AccountName = order.Account.Name,
                SalesmanName = $"{order.Salesman.FirstName} {order.Salesman.LastName}",
                Date = order.Date,
                TotalSales = order.TotalSales,
            };

            return Ok(dto);
        }

        // PUT: api/orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, CreateOrderDTO dto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            order.AccountId = dto.AccountId;
            order.SalesmanId = dto.SalesmanId;
            order.Date = dto.Date;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(CreateOrderDTO dto)
        {
            var order = new Order
            {
                AccountId = dto.AccountId,
                SalesmanId = dto.SalesmanId,
                Date = dto.Date,
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            order = await _context.Orders
            .Include(a => a.Salesman)
            .FirstAsync(a => a.Id == order.Id);

            var resultDto = new OrderDto
            {
                Id = order.Id,
                AccountName = order.Account.Name,
                SalesmanName = $"{order.Salesman.FirstName} {order.Salesman.LastName}",
                Date = order.Date,
                TotalSales = order.TotalSales,
            };

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
