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
        public CryptoService()
        {
            _httpClientHelper = new HttpClientHelper();
        }
        public void FullCrypto(Action<string> action) => _httpClientHelper.Get<ObservableCollection<Model.Cryptocurrencies>>("https://api.coincap.io/v2/assets", action);
        public void MarketsCrypto(Action<string> action, String id) => _httpClientHelper.Get<Markets>($"https://api.coincap.io/v2/assets/{id}/markets", action);
        public void HistoryCrypto(Action<string> action, String id) => _httpClientHelper.Get<List<PriceData>>($"https://api.coincap.io/v2/assets/{id}/history?interval=d1", action);
    }
}
