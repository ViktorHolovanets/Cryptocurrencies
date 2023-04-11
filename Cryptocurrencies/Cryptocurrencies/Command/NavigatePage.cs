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

        public NavigatePage()
        {
            Frame = new Frame();
            Frame.Navigate(new Page1());
            // Ініціалізація команди
            NavigateCommand = new RelayCommand(Navigate);
        }

        private void Navigate(object parameter)
        {
            Frame.Navigate(new Home());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
