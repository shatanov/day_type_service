namespace DayTypeService.Services
{
    public class CalendarService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<List<DayInfo>> GetCalendarDataAsync(int year)
        {
            var response = await client.GetStringAsync($"https://isdayoff.ru/api/getdata?year={year}&pre=1");

            var dayInfos = new List<DayInfo>();
            for (int i = 0; i < response.Length; i++)
            {
                var date = new DateTime(year, 1, 1).AddDays(i).ToUniversalTime();
                var dayType = response[i] switch
                {
                    '0' => "Рабочий",
                    '1' => "Выходной",
                    '2' => "Сокращённый",
                    _ => "Неизвестный"
                };

                dayInfos.Add(new DayInfo
                {
                    Date = date,
                    DayOfWeek = date.DayOfWeek,
                    DayType = dayType
                });
            }

            return dayInfos;
        }
    }
}
