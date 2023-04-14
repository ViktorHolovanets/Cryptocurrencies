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
            { }
        }

        //private ICommand allCryptoCommand;
        //public ICommand AllCryptoCommand
        //{
        //    get { return allCryptoCommand ?? (allCryptoCommand = new AsyncRelayCommand(AllInfoCrypto)); }
        //}
        //private async Task AllInfoCrypto(object obj = null)
        //{
        //    Cryptocurrencies = new ObservableCollection<Model.Cryptocurrencies>(CryptoRepository.GetInstance().Cryptocurrencies.ToList());
        //}
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

        private bool isTrueChecked;
        public bool IsTrueChecked
        {
            get { return isTrueChecked; }
            set
            {
                if (isTrueChecked != value)
                {
                    isTrueChecked = value;
                    OnPropertyChanged(); // Виклик методу для повідомлення про зміну властивості
                }
            }
        }
        private ICommand checkedInfoCommand;
        public ICommand CheckedInfoCommand
        {
            get { return checkedInfoCommand ?? (checkedInfoCommand = new AsyncRelayCommand(Checked, CanExecuteChecked)); }
        }

        private bool isCommandExecuting;
        private bool CanExecuteChecked()
        {
            return !isCommandExecuting; // Перевірка, чи команда не виконується
        }
        private async Task Checked(object obj)
        {
            try
            {
                isCommandExecuting = true; // Встановлення статусу виконання команди
                OnPropertyChanged(nameof(CheckedInfoCommand)); // Повідомлення про можливість виконання команди
                                                               // Ваша довготривала асинхронна операція тут
                if (IsTrueChecked)
                {
                    Cryptocurrencies = new ObservableCollection<Model.Cryptocurrencies>(CryptoRepository.GetInstance().Cryptocurrencies.ToList());
                }
                else
                    Cryptocurrencies = new ObservableCollection<Model.Cryptocurrencies>(CryptoRepository.GetInstance().Cryptocurrencies.Take(10).ToList());
            }
            finally
            {
                isCommandExecuting = false; // Зняття статусу виконання команди
                OnPropertyChanged(nameof(CheckedInfoCommand)); // Повідомлення про можливість виконання команди
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

