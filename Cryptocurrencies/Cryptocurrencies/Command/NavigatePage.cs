using Cryptocurrencies.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Cryptocurrencies.Command
{
    public class NavigatePage : INotifyPropertyChanged
    {
  
        private static NavigatePage _instance;

        // existing object stored in the static field.
        public static NavigatePage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NavigatePage();
            }
            return _instance;
        }

        private Frame _frame;
        public Frame Frame
        {
            get { return _frame; }
            set
            {
                _frame = value;
                OnPropertyChanged(nameof(Frame));
            }
        }

        public ICommand NavigateCommand { get; set; }

        private NavigatePage()
        {
            Frame = new Frame();
            Frame.Navigate(new Home());
            NavigateCommand = new AsyncRelayCommand(Navigate);
        }

        private async Task Navigate(object parameter)
        {
            Frame.Navigate(new Uri($"/Pages/{parameter}.xaml", UriKind.Relative));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
