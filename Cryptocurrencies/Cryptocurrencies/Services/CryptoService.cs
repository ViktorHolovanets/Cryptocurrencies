using Cryptocurrencies.Model;
using Cryptocurrencies.Services.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrencies.Services
{
    public class CryptoService
    {
        private HttpClientHelper _httpClientHelper;
        private string baseUrl { get; set; } = "https://api.coincap.io/v2";
        public CryptoService()
        {
            _httpClientHelper = new HttpClientHelper();
        }
        public void FullCrypto(Action<string> action) => _httpClientHelper.Get<List<Model.Cryptocurrencies>>($"{baseUrl}/assets", action);
        public void MarketsCrypto(Action<string> action, String id) => _httpClientHelper.Get<List<Market>>($"{baseUrl}/assets/{id}/markets", action);
        public void HistoryCrypto(Action<string> action, String id) => _httpClientHelper.Get<List<PriceData>>($"{baseUrl}/assets/{id}/history?interval=d1", action);
        public void RaresCrypto(Action<string> action) => _httpClientHelper.Get<List<Rate>>($"{baseUrl}/rates", action);
    }
}
