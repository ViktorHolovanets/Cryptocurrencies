using Cryptocurrencies.Command;
using Cryptocurrencies.Pages;
using Cryptocurrencies.Properties;
using Cryptocurrencies.Repositories;
using Cryptocurrencies.Services.Http;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Cryptocurrencies.ViewModel
{
    public class CryptocurrenciesViewModel : INotifyPropertyChanged
    {
        private Model.Cryptocurrencies selectedCrypto;
        public Model.Cryptocurrencies SelectedCrypto
        {
            get { return selectedCrypto; }
            set
            {
                selectedCrypto = value;
                OnPropertyChanged("SelectedCrypto");
            }
        }
        private ObservableCollection<Model.Cryptocurrencies> cryptocurrencies;
        public ObservableCollection<Model.Cryptocurrencies> Cryptocurrencies
        {
            get { return cryptocurrencies; }
            set
            {
                cryptocurrencies = value;
                OnPropertyChanged("Cryptocurrencies");
            }
        }

        public CryptocurrenciesViewModel()
        {
            CurrentResources("Theme/DarkTheme");
            Loader();
        }
        private void Loader()
        {
            Task.Run(async () =>
            {
                int count = 0;
                while (count < 5)
                {
                    if (CryptoRepository.GetInstance().Cryptocurrencies != null)
                    {
                        Cryptocurrencies = new ObservableCollection<Model.Cryptocurrencies>(CryptoRepository.GetInstance().Cryptocurrencies.Take(10).ToList());
                        count = 5;
                    }
                    else
                    {
                        await Task.Delay(2000);
                        count++;
                    }
                }
            });
        }
        private bool isTheme = true;
        private ICommand _toggleThemeCommand;
        public ICommand ToggleThemeCommand
        {
            get { return _toggleThemeCommand ?? (_toggleThemeCommand = new AsyncRelayCommand(ToggleTheme)); }
        }
       
        private ICommand infoCommand;
        public ICommand InfoCommand
        {
            get { return infoCommand ?? (infoCommand = new AsyncRelayCommand(InfoCrypto)); }
        }
        private async Task InfoCrypto(object obj = null)
        {
            NavigatePage.GetInstance().Frame.Navigate(new Info(SelectedCrypto.Id));
        }
        private async Task ToggleTheme(object obj)
        {

            if (isTheme)
            {
                CurrentResources("Theme/LightTheme");
            }
            else
            {
                CurrentResources("Theme/DarkTheme");
            }
            isTheme = !isTheme;
        }
        private ICommand allCryptoCommand;
        public ICommand AllCryptoCommand
        {
            get { return allCryptoCommand ?? (allCryptoCommand = new AsyncRelayCommand(AllInfoCrypto)); }
        }
        private async Task AllInfoCrypto(object obj = null)
        {
            Cryptocurrencies = new ObservableCollection<Model.Cryptocurrencies>(CryptoRepository.GetInstance().Cryptocurrencies.ToList());
        }
        private void CurrentResources(string style)
        {
            var uri = new Uri(style + ".xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}

