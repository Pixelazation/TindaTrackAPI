using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TindaTrackAPI.DTOs.Account;
using TindaTrackAPI.Models;

namespace TindaTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly TindaTrackContext _context;

        public AccountsController(TindaTrackContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts()
        {
            var account = await _context.Accounts
            .Select(account => new AccountDto
            {
                Id = account.Id,
                Name = account.Name,
                Address = account.Address,
                BarangayName = account.Barangay.Name,
                MunicipalityName = account.Barangay.Municipality.Name
            })
            .ToListAsync();

            return Ok(account);
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            var dto = new AccountDto
            {
                Id = account.Id,
                Name = account.Name,
                Address = account.Address,
                BarangayName = account.Barangay.Name,
                MunicipalityName = account.Barangay.Municipality.Name
            };

            return dto;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, CreateAccountDto dto)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return NotFound();

            account.Name = dto.Name;
            account.Address = dto.Address;
            account.BarangayId = dto.BarangayId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountDto>> PostAccount(CreateAccountDto dto)
        {
            var account = new Account
            {
                Name = dto.Name,
                Address = dto.Address,
                BarangayId = dto.BarangayId,
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            account = await _context.Accounts
            .Include(a => a.Barangay)
            .ThenInclude(b => b.Municipality)
            .FirstAsync(a => a.Id == account.Id);

            var resultDto = new AccountDto
            {
                Id = account.Id,
                Name = account.Name,
                Address = account.Address,
                BarangayName = account.Barangay.Name,
                MunicipalityName = account.Barangay.Municipality.Name
            };

            return CreatedAtAction("GetAccount", new { id = account.Id }, resultDto);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
