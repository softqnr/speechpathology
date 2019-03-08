using SQLite;
using System;
using System.Threading.Tasks;

namespace SpeechPathology.Models
{
    public class AgeCalculation : ModelBase
    {
        public DateTime BirthDate { get; set; }
        public DateTime TestDate { get; set; }

        public int AgeInYears { get; set; }
        public int MonthsThisYear { get; set; }
        public int DaysThisMonth { get; set; }
        public int DaysThisYear { get; set; }

        public readonly string LanguageSkillsBase = "languageskills_";
        public readonly string SpeechSoundsBase = "speechsounds_";

        public void Calculate()
        {
            DateTime d;
            var start = BirthDate;
            var end = TestDate;

            AgeInYears = 0;
            d = start;
            while (d <= end)
            {
                AgeInYears++;
                d = start.AddYears(AgeInYears);
            }

            AgeInYears--;
            start = start.AddYears(AgeInYears);

            MonthsThisYear = 0;
            d = start;
            while (d <= end)
            {
                MonthsThisYear++;
                d = start.AddMonths(MonthsThisYear);
            }

            MonthsThisYear--;
            start = start.AddMonths(MonthsThisYear);

            DaysThisMonth = 0;
            d = start;
            while (d <= end)
            {
                DaysThisMonth++;
                d = start.AddDays(DaysThisMonth);
            }

            DaysThisMonth--;
        }
    }
}
