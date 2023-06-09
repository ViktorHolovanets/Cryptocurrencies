﻿using Cryptocurrencies.Command;
using Cryptocurrencies.Model;
using Cryptocurrencies.Repositories;
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
using System.Windows.Input;

namespace Cryptocurrencies.ViewModel
{
    public class CryptoInfoViewModel : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public Model.Cryptocurrencies CurrentCrypto { get; set; }
        public Model.Cryptocurrencies SelectCrypto { get; set; }
        public List<Model.Cryptocurrencies> DB { get; set; }
        private List<PriceData> priceDataList;
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public SeriesCollection SeriesMarketsCollection { get; set; }
        public List<string> LabelsMarkets { get; set; }
        private DateTime selectedStartDate;
        public DateTime SelectedStartDate
        {
            get { return selectedStartDate; }
            set
            {
                if (selectedStartDate != value)
                {
                    selectedStartDate = value;
                    OnPropertyChanged("SelectedStartDate");
                }
            }
        }
        private string resultExchange;
        public string ResultExchange
        {
            get { return resultExchange; }
            set
            {
                if (resultExchange != value)
                {
                    resultExchange = value;
                    OnPropertyChanged("ResultExchange");
                }
            }
        }
        private DateTime selectedEndDate;
        public DateTime SelectedEndDate
        {
            get { return selectedEndDate; }
            set
            {
                if (selectedEndDate != value)
                {
                    selectedEndDate = value;
                    OnPropertyChanged("SelectedEndDate");
                }
            }
        }

        public CryptoInfoViewModel(string id)
        {
            Id = id;
            Load();
            ViewPeriod = new RelayCommand(ExecuteViewPeriod);
            DB = CryptoRepository.GetInstance().Cryptocurrencies;
            CurrentCrypto = DB.FirstOrDefault(cr => cr.Id == id);
        }
        public void Load()
        {
            var client = new CryptoService();
            client.MarketsCrypto(data =>
            {
                var priceMarkets = JsonConvert.DeserializeObject<List<Market>>(data);
                if (priceMarkets != null && priceMarkets.Count > 0)
                {
                    Application.Current.Dispatcher.Invoke(() => LoaderMarketsPrice(priceMarkets));
                }
            }, Id);
            client.HistoryCrypto(data =>
            {
                priceDataList = JsonConvert.DeserializeObject<List<PriceData>>(data);
                if (priceDataList != null && priceDataList.Count > 0)
                {
                    var dataPrice = PriceData.GetLast10PriceData(priceDataList);
                    SelectedStartDate = dataPrice.FirstOrDefault().Date;
                    SelectedEndDate = dataPrice.Last().Date;
                    Application.Current.Dispatcher.Invoke(() => LoaderHistory(dataPrice));
                }
            }, Id);
        }
        public void LoaderHistory(List<PriceData> priceDataList)
        {
            var prices = new ChartValues<double>(priceDataList.Select(pd => pd.PriceUsd).ToList());
            Labels = new List<string>(priceDataList.Select(pd => pd.Date.ToString("yyyy-MM-dd")).ToList());

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Price (USD)",
                    Values = prices
                }
            };
            OnPropertyChanged(nameof(SeriesCollection));
            OnPropertyChanged(nameof(Labels));
        }
        public void LoaderMarketsPrice(List<Market> priceMarkets)
        {
            var prices = new ChartValues<decimal>(priceMarkets.Select(pm => pm.PriceUsd).ToList());
            LabelsMarkets = new List<string>(priceMarkets.Select(pm => pm.ExchangeId).ToList());

            SeriesMarketsCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Ціна (USD)",
                    Values = prices
                }
            };
            OnPropertyChanged(nameof(SeriesMarketsCollection));
            OnPropertyChanged(nameof(LabelsMarkets));
        }
        private string itemsCountCrypto="1";
        public string ItemsCountCrypto
        {
            get { return itemsCountCrypto; }
            set
            {
                if (itemsCountCrypto != value)
                {
                    itemsCountCrypto = value;
                    OnPropertyChanged("ItemsCountCrypto");
                }
            }
        }
        public ICommand ViewPeriod { get; }
        private void ExecuteViewPeriod(object parameter)
        {
            LoaderHistory(PriceData.GetPriceDataBetweenDates(priceDataList, SelectedStartDate, SelectedEndDate));
        }

        private ICommand resultExchangeCommand;
        public ICommand ResultExchangeCommand
        {
            get { return resultExchangeCommand ?? (resultExchangeCommand = new AsyncRelayCommand(Exchange)); }
        }
        private async Task Exchange(object obj)
        {
            int result;
            if(SelectCrypto!=null&& int.TryParse(ItemsCountCrypto, out result))
            {
                decimal exchangedAmount = result * CurrentCrypto.PriceUsd / SelectCrypto.PriceUsd;
                ResultExchange = $"{exchangedAmount.ToString("0.00000")} {SelectCrypto.Symbol}";
            }            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
