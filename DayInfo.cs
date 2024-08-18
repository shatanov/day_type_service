using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DayTypeService
{
    public class DayInfo
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string DayType { get; set; } // Рабочий, Выходной, Сокращённый
    }
}
