using Cryptocurrencies.Command;
using Cryptocurrencies.Pages;
using Cryptocurrencies.Properties;
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
        private  Model.Cryptocurrencies selectedCrypto;
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
                OnPropertyChanged("Cryptocurrencies"); // Виклик події OnPropertyChanged
            }
        }

        public CryptocurrenciesViewModel()
        {
            CurrentResources("Theme/DarkTheme");
            var  httpClientHelper = new HttpClientHelper();
            httpClientHelper.Get<ObservableCollection<Model.Cryptocurrencies>>("https://api.coincap.io/v2/assets", met);
        }
        private void met(string data)
        {
            Cryptocurrencies = JsonConvert.DeserializeObject<ObservableCollection<Model.Cryptocurrencies>>(data);
        }
        private bool isTheme = true;
        private ICommand _toggleThemeCommand;
        public ICommand ToggleThemeCommand
        {
            get { return _toggleThemeCommand ?? (_toggleThemeCommand = new RelayCommand(ToggleTheme)); }
        }
        private void ToggleTheme(object obj)
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

