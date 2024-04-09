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
using System.Windows.Shapes;

namespace NB4DC3_HFT_2023241.WpfClient
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void BrandButton_ClickB(object sender, RoutedEventArgs e)
        {
            BrandEditor newWindow = new BrandEditor();
            newWindow.Show();
        }

        private void CarButton_ClickC(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
        }

        private void OrderButton_ClickO(object sender, RoutedEventArgs e)
        {
            OrderEditor newWindow = new OrderEditor();
            newWindow.Show();
        }
    }
}
