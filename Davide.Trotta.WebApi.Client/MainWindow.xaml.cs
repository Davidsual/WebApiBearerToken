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
using Davide.Trotta.WebApi.Client.Model;
using Newtonsoft.Json;

namespace Davide.Trotta.WebApi.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ServiceRest _serviceRest;
        private Token Token { get; set; }
        public MainWindow()
        {
            _serviceRest= new ServiceRest();
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Token = await _serviceRest.GetTokenAsync();

            this.TokenDisplay.Content = Token.AccessToken;
            this.Expiration.Content = string.Format("Expire in: {0}", Token.ExpiresIn);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string result = await _serviceRest.WhoIAm(Token);

            this.Response.Content = JsonConvert.SerializeObject(result, Formatting.Indented); ;
        }


    }
}
