using Cryptocurrencies.Command;
using Cryptocurrencies.Pages;
using Cryptocurrencies.Repositories;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

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
        private ICommand infoCommand;
        public ICommand InfoCommand
        {
            get { return infoCommand ?? (infoCommand = new AsyncRelayCommand(InfoCrypto)); }
        }
        private async Task InfoCrypto(object obj = null)
        {
            try
            {
                if (obj != null)
                    NavigatePage.GetInstance().Frame.Navigate(new Info(SelectedCrypto.Id));
            }
            catch (Exception)
            {}
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
        private string searchCrypto;
        public string SearchCrypto
        {
            get { return searchCrypto; }
            set
            {
                searchCrypto = value;
                OnPropertyChanged("SearchCrypto");
            }
        }

        private ICommand findCryptoCommand;
        public ICommand FindCryptoCommand
        {
            get { return findCryptoCommand ?? (findCryptoCommand = new AsyncRelayCommand(SearchResult)); }
        }
        private async Task SearchResult(object obj)
        { 
            Cryptocurrencies = new ObservableCollection<Model.Cryptocurrencies>(CryptoRepository.GetInstance().Cryptocurrencies.Where(c =>
            c.Name.ToLower().Contains(SearchCrypto) ||
            c.Symbol.ToLower().Contains(SearchCrypto))
            .ToList());
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}

