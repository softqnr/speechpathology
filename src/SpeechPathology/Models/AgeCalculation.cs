using SQLite;
using System;
using System.Threading.Tasks;

namespace SpeechPathology.Models
{
    public class AgeCalculation : ModelBase
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int AgeInYears { get; set; }
        public int MonthsThisYear { get; set; }
        public int DaysThisMonth { get; set; }
        public int TotalDays { get; set; }
        public int DaysThisYear { get; set; }

        public readonly string LanguageSkillsBase = "languageskills_";
        public readonly string SpeechSoundsBase = "speechsounds_";

        public AgeCalculation()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public void Calculate()
        {
            DateTime d;
            var start = StartDate;
            var end = EndDate;

            TotalDays = EndDate.Subtract(StartDate).Days;

            AgeInYears = 0;
            d = start;
            while (d <= end)
            {
                AgeInYears++;
                d = start.AddYears(AgeInYears);
            }
            AgeInYears--;
            start = start.AddYears(AgeInYears);

            DaysThisYear = 0;
            d = start;
            while (d <= end)
            {
                DaysThisYear++;
                d = start.AddDays(DaysThisYear);
            }
            DaysThisYear--;

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
