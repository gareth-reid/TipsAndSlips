using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Entities
{
    public class MultiBuilder
    {
        public List<Market> Markets;
        public List<Market> FinalMarkets;
        public string MultiName { get; set; }
        public double TotalPrice { get; set; }
        public int MultiBuilderId;
        public MultiBuilder()
        {
            FinalMarkets = new List<Market>();
        }

        public List<Market> Execute()
        {
            var events = Markets.GroupBy(market => market.EventId);
            foreach (IGrouping<String, Market> e in events)
            {
                var orderedByRationMarkets = e;//.Where(m => (m.BackLayRatio > 0 && m.BackLayRatio < 0.9) && m.Price < 3);
                if (orderedByRationMarkets.Count() > 0)
                {
                    FinalMarkets.Add(orderedByRationMarkets.OrderBy(m => m.BackLayRatio).First());
                }
                var a = 0;
            }
            
            return FinalMarkets;
        }

        public String GetTotalPrice()
        {
            String specifier = "C";
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-AU");
            return TotalPrice.ToString(specifier, culture);
        }

    }
}
