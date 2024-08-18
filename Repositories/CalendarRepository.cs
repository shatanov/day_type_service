using DayTypeService.DbContext;

namespace DayTypeService.Repositories
{
    public class CalendarRepository
    {
        private readonly ApplicationDbContext _context;

        public CalendarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveCalendarDataAsync(List<DayInfo> dayInfos)
        {
            _context.Days.AddRange(dayInfos);
            await _context.SaveChangesAsync();
        }
    }
}
