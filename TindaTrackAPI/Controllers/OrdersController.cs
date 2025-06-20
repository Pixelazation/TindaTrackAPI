using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using TindaTrackAPI.DTOs.Item;
using TindaTrackAPI.DTOs.Order;
using TindaTrackAPI.DTOs.Purchase;
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
                AccountId = order.AccountId,
                AccountName = order.Account.Name,
                SalesmanId = order.SalesmanId,
                SalesmanName = $"{order.Salesman.FirstName} {order.Salesman.LastName}",
                Date = order.Date,
                TotalSales = order.Purchases.Sum(p => p.Quantity * p.UnitPrice)
            })
            .ToListAsync();

            return Ok(orders);
        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Account)
                .Include(o => o.Salesman)
                .Include(o => o.Purchases)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var dto = new OrderDto
            {
                Id = order.Id,
                AccountId = order.AccountId,
                AccountName = order.Account.Name,
                SalesmanId = order.SalesmanId,
                SalesmanName = $"{order.Salesman.FirstName} {order.Salesman.LastName}",
                Date = order.Date,
                TotalSales = order.Purchases.Sum(p => p.Quantity * p.UnitPrice)
            };

            return Ok(dto);
        }

        // GET: api/orders/{id}/purchases
        [HttpGet("{id}/purchases")]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetOrderPurchases(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Purchases)
                    .ThenInclude(p => p.Item)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var purchases = order.Purchases.Select(p => new PurchaseDto
            {
                Item = new ItemDto
                {
                    Id = p.Item.Id,
                    ItemCode = p.Item.ItemCode,
                    Name = p.Item.Name,
                    Description = p.Item.Description,
                    UnitPrice = p.Item.UnitPrice
                },
                Quantity = p.Quantity,
                UnitPrice = p.UnitPrice,
                TotalAmount = p.TotalAmount
            }).ToList();

            return Ok(purchases);
        }


        // PUT: api/orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, CreateOrderDTO dto)
        {
            var order = await _context.Orders
            .Include(o => o.Purchases)
            .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            order.AccountId = dto.AccountId;
            order.SalesmanId = dto.SalesmanId;
            order.Date = dto.Date;

            _context.Purchases.RemoveRange(order.Purchases);

            order.Purchases = dto.Purchases.Select(p => new Purchase
            {

                ItemId = p.ItemId,
                Quantity = p.Quantity,
                UnitPrice = p.UnitPrice,
            }).ToList();

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
                Purchases = dto.Purchases.Select(p => new Purchase
                {
                    ItemId = p.ItemId,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            order = await _context.Orders
            .Include(a => a.Salesman)
            .Include(a => a.Purchases)
            .Include(a => a.Account)
            .FirstAsync(a => a.Id == order.Id);

            var resultDto = new OrderDto
            {
                Id = order.Id,
                AccountId = order.AccountId,
                AccountName = order.Account.Name,
                SalesmanId = order.SalesmanId,
                SalesmanName = $"{order.Salesman.FirstName} {order.Salesman.LastName}",
                Date = order.Date,
                TotalSales = order.Purchases.Sum(p => p.Quantity * p.UnitPrice)
            };

            return CreatedAtAction("GetOrder", new { id = order.Id }, resultDto);
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
