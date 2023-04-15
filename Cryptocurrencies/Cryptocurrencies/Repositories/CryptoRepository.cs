using Cryptocurrencies.Command;
using Cryptocurrencies.Model;
using Cryptocurrencies.Services;
using Cryptocurrencies.Services.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrencies.Repositories
{
    public class CryptoRepository
    {
        public List<Model.Cryptocurrencies> Cryptocurrencies { get; set; }
        public List<Market> Markets { get; set; }
        public List<Rate> Rates { get; set; }
        private static CryptoRepository _instance;
        public CryptoService cryptoService { get; set; }
        // existing object stored in the static field.
        public static CryptoRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CryptoRepository();
            }
            return _instance;
        }
        private CryptoRepository()
        {
            Loader();
        }
        private void Loader()
        {
            try
            {
                cryptoService = new CryptoService();

                Task.Run(() =>
                {
                    while (true)
                    {
                        cryptoService.FullCrypto(data =>
                        {
                            Cryptocurrencies = JsonConvert.DeserializeObject<List<Model.Cryptocurrencies>>(data);
                        });                       
                        if (Rates == null)
                            cryptoService.RaresCrypto(data =>
                            {
                                Rates = JsonConvert.DeserializeObject<List<Rate>>(data);
                            });
                        Task.Delay(30000);
                    }
                });
            }
            catch (Exception)
            {
            }


        }
    }
}
