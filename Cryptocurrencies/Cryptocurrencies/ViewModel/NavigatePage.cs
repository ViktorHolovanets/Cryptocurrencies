using Cryptocurrencies.Pages;
using Cryptocurrencies.Services.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            CurrentResources("Theme/DarkTheme");
            NavigateCommand = new AsyncRelayCommand(Navigate);
        }

        private async Task Navigate(object parameter)
        {
            Frame.Navigate(new Uri($"/Pages/{parameter}.xaml", UriKind.Relative));
        }
        private bool isTheme = true;
        private ICommand _toggleThemeCommand;
        public ICommand ToggleThemeCommand
        {
            get { return _toggleThemeCommand ?? (_toggleThemeCommand = new AsyncRelayCommand(ToggleTheme)); }
        }
        private async Task ToggleTheme(object obj)
        {
            var theme = isTheme ? CurrentResources("Theme/LightTheme") : CurrentResources("Theme/DarkTheme");
            isTheme = !isTheme;
        }
        private bool CurrentResources(string style)
        {
            var uri = new Uri(style + ".xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
