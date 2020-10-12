using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Entities
{
    public class Market : IComparable<Market>
    {        
        public String EventName;
        public String EventId;
        public String MarketName;
        public String MarketId;
        public String EventType;
        public String Competition;
        public String Date;
        public String Selection;
        public String Status;
        public String Urgency;
        public String GetUrgency()
        {
            GetTimeRemaining();
            return Urgency;
        }

        public String GetPrice()
        {
            String specifier = "C";
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-AU");
            return Price.ToString(specifier, culture);            
        }

        public String GetTimeRemaining()
        {
            bool past = false;
            string timeDisplay = String.Empty;
            DateTime date = DateTime.Parse(Date);            
            var remaining = date - DateTime.Now.ToUniversalTime();
            
            var totalMinutes = remaining.TotalMinutes;
            if (totalMinutes <= 0)
            {
                totalMinutes *= -1;
                past = true;
            }
            var hours = Math.Floor(totalMinutes / 60);
            var minutes = totalMinutes - (hours * 60);

            if (hours > 24)
            {
                timeDisplay = Math.Floor(remaining.TotalDays) + " days";
            }
            else if (hours > 0)
            {
                Urgency = "info";
                timeDisplay = hours + " hour(s) & " + Math.Floor(minutes) + " minutes";
            }
            else
            {
                Urgency = "custom";
                timeDisplay += Math.Floor(minutes) + " minutes";
            }
            return (past ? "-" : "") + timeDisplay;
        }

        public int CompareTo([AllowNull] Market other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return this.BackLayRatio.CompareTo(other.BackLayRatio);
            }
        }

        public long SelectionId;        
        public double? Handicap;
        public int Order;

        public Double? LastPriceTraded;
        public Double Price;
        
        public Double BackLayRatio;

        public Market()
        {
        }
    }
}
