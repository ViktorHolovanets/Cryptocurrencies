using Cryptocurrencies.Model;
using Cryptocurrencies.Services;
using Cryptocurrencies.Services.Http;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cryptocurrencies.ViewModel
{
    public class CryptoInfoViewModel : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public ObservableCollection<double> ChartValues { get; private set; }
        public ObservableCollection<string> Labels { get; private set; }
        public CryptoInfoViewModel(string id)
        {
            Id = id;
            Load();
        }
        public void Load()
        {
            var client = new CryptoService();
            client.HistoryCrypto(data => {
                List<PriceData> priceDataList = JsonConvert.DeserializeObject<List<PriceData>>(data);
                if (priceDataList != null && priceDataList.Count > 0)
                {
                    Application.Current.Dispatcher.Invoke(() => Loader(priceDataList));
                }
            }, Id);

        }
        public void Loader(List<PriceData> priceDataList)
        {
            ChartValues = new ObservableCollection<double>(priceDataList.Select(pd => pd.PriceUsd).ToList());
            Labels = new ObservableCollection<string>(priceDataList.Select(pd => pd.Date.ToString("yyyy-MM-dd")).ToList());
            OnPropertyChanged(nameof(ChartValues));
            OnPropertyChanged(nameof(Labels));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
