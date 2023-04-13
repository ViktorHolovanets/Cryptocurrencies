using Cryptocurrencies.Command;
using Cryptocurrencies.Pages;
using Cryptocurrencies.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Cryptocurrencies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext =NavigatePage.GetInstance();


        }
        private void NavigateToPage(Page page)
        {
            MainFrame.Content = null;
            MainFrame.Content = page;
        }
    }
}
