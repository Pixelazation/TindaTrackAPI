using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TindaTrackAPI.DTOs.Barangay;
using TindaTrackAPI.Models;

namespace TindaTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarangaysController : ControllerBase
    {
        private readonly TindaTrackContext _context;

        public BarangaysController(TindaTrackContext context)
        {
            _context = context;
        }

        // GET: api/Barangays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BarangayDto>>> GetBarangays()
        {
            var barangays = await _context.Barangays
            .Select(barangay => new BarangayDto
            {
                Id = barangay.Id,
                Name = barangay.Name,
            })
            .ToListAsync();

            return Ok(barangays);
        }
    }
}
