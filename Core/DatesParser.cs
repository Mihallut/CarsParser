using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarsParser.Core
{
    public static class DatesParser
    {
        public static void ParseDates(string dates, out DateTime startDate, out DateTime? endDate)
        {
            Regex regex = new Regex(@"(\d{2}\.\d{4})");

            // Извлечение второй даты из строки
            MatchCollection matches = regex.Matches(dates);
            if (matches.Count >= 2)
            {
                string start = matches[0].Groups[1].Value;
                string end = matches[1].Groups[1].Value;

                // Преобразование строки в DateTime
                endDate = DateTime.ParseExact(end, "MM.yyyy", null);
                startDate = DateTime.ParseExact(start, "MM.yyyy", null);
            }
            else
            {
                string start = matches[0].Groups[1].Value;
                startDate = DateTime.ParseExact(start, "MM.yyyy", null);

                endDate = null;
            }


        }

    }
}
