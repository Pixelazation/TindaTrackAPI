using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TindaTrackAPI.DTOs.Municipality;
using TindaTrackAPI.Models;

namespace TindaTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly TindaTrackContext _context;

        public MunicipalitiesController(TindaTrackContext context)
        {
            _context = context;
        }

        // GET: api/Municipalities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MunicipalityDto>>> GetMunicipalities()
        {
            var municipalities = await _context.Municipalities
            .Select(municipality => new MunicipalityDto
            {
                Id = municipality.Id,
                Name = municipality.Name,
            })
            .ToListAsync();

            return Ok(municipalities);
        }
    }
}
