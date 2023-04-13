using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrencies.Model
{
    public class PriceData
    {
        public double PriceUsd { get; set; }
        public long Time { get; set; }
        public DateTime Date { get; set; }

        public static List<PriceData> GetPriceDataBetweenDates(List<PriceData> priceDataList, DateTime startDate, DateTime endDate)
        {
            List<PriceData> filteredData = priceDataList.Where(pd => pd.Date >= startDate && pd.Date <= endDate).ToList();
            return filteredData;
        }
        public static List<PriceData> GetLast10PriceData(List<PriceData> priceDataList)
        {
            List<PriceData> last10Data = priceDataList.Skip(Math.Max(0, priceDataList.Count - 10)).Take(10).ToList();
            return last10Data;
        }

    }

}
