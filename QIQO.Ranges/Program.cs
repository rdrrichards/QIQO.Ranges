Console.WriteLine("Hello, World!");

var hCodes = new string[] { "NYD", "IND", "CMX" };
var cha = DateRange(DateTime.Today.AddMonths(-1), DateTime.Today.AddMonths(36));

Console.ReadLine();

IEnumerable<Holiday> DateRange(DateTime fromDate, DateTime toDate)
{
    return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1)
                     .Select(d => fromDate.AddDays(d))
                     .Select(a => new Holiday(a, GetHolidayCode(a), ""))
                     .Where(t => t.HolidayCode != "")
                     .Select(e => e with { 
                         ObservedDate = hCodes.Contains(e.HolidayCode) && 
                            (e.ObservedDate.DayOfWeek == DayOfWeek.Saturday || e.ObservedDate.DayOfWeek == DayOfWeek.Sunday) ? 
                            e.ObservedDate.AddDays(e.ObservedDate.DayOfWeek == DayOfWeek.Saturday ? -1 : 1) :
                            e.ObservedDate,
                         HolidayName = GetHolidayName(e.HolidayCode),
                     });
}

string GetHolidayCode(DateTime dateTime) => dateTime switch
{
    { Month: 1, Day: 1 } => "NYD",
    { Month: 1, DayOfWeek: DayOfWeek.Monday, Day: >= 15, Day: <= 20 } => "MLK",
    { Month: 5, DayOfWeek: DayOfWeek.Monday, Day: >= 20 } => "MEM",
    { Month: 7, Day: 4 } => "IND",
    { Month: 9, DayOfWeek: DayOfWeek.Monday, Day: < 8 } => "LAB",
    { Month: 11, DayOfWeek: DayOfWeek.Thursday, Day: >= 21, Day: <= 28 } => "TGD",
    { Month: 12, Day: 25 } => "CMX",
    _ => ""
};

string GetHolidayName(string holidayCode) => holidayCode switch
{
    "NYD" => "New Year Day",
    "MLK" => "Martin Luther King Day",
    "MEM" => "Memorial Day",
    "IND" => "Independence Day",
    "LAB" => "Labor Day",
    "TGD" => "Thanksgiving Day",
    "CMX" => "Christmas Day",
    _ => "Unknown Holiday"
};

record Holiday(DateTime ObservedDate, string HolidayCode, string HolidayName);