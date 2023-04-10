using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cryptocurrencies.ViewModel
{
    public class CryptocurrenciesViewModel : INotifyPropertyChanged
    {
       
        public ObservableCollection<Model.Cryptocurrencies> cryptocurrencies { get; set; }=new ObservableCollection<Model.Cryptocurrencies>();

        public CryptocurrenciesViewModel()
        {
            var baseAddress = "https://api.coincap.io/v2/assets";
            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync(baseAddress).Result)
                {
                    var customerJsonString = response.Content.ReadAsStringAsync().Result;
                    var json = JObject.Parse(customerJsonString);
                    var dataArray = json["data"].ToString();
                    cryptocurrencies = JsonConvert.DeserializeObject<ObservableCollection<Model.Cryptocurrencies>>(dataArray);
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}

