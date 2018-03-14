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
using System.Net;
using System.Net.Sockets;

namespace WinAppClient
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ContentView_Loaded(object sender, RoutedEventArgs e)
        {
            Welcome welcome = new Welcome();
            ContentPanel.Children.Add(welcome);
        }

        private void CategoryBar_Loaded(object sender, RoutedEventArgs e)
        {
            CategoryBar.Items.Add(new CategoriObj("C/C++"));
            CategoryBar.Items.Add(new CategoriObj("Java"));
            CategoryBar.Items.Add(new CategoriObj("C#/.Net"));
            CategoryBar.Items.Add(new CategoriObj("Web"));
        }

        private void TB_Search_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_Search.Text = null;
            TB_Search.Foreground = Brushes.Black;
        }
    }
}
