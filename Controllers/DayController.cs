using DayTypeService.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DayTypeService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DayController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DayController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{date}")]
        public async Task<IActionResult> GetDayType([FromRoute] string date)
        {
            if (!DateTime.TryParse(date, out var parsedDate))
            {
                return BadRequest("Некорректный формат даты.");
            }

            parsedDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);

            var dayInfo = await _context.Days
                .FirstOrDefaultAsync(d => d.Date.Date == parsedDate.Date);

            if (dayInfo == null)
            {
                return NotFound("Данные о дне не найдены.");
            }

            return Ok(dayInfo.DayType);
        }
    }
}
