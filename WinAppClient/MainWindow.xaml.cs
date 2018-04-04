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
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace WinAppClient
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowHandler windowHandler;

        public MainWindow()
        {
            InitializeComponent();
            windowHandler = new MainWindowHandler();
        }

        private void ContentView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentPanel.Children.Add(new Welcome());
        }

        private void CategoryBar_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(var idx in windowHandler.GetCategoriObjs())
            {
                CategoryBar.Items.Add(idx);
            }
        }

        private void TB_Search_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_Search.Text = null;
            TB_Search.Foreground = Brushes.Black;
        }

        private void BT_Search_Submit_Click(object sender, RoutedEventArgs e)
        {
            var jsonList = new List<JObject>();

            TB_Search.Text = null;
            string searchKeyword = TB_Search.Text;
            //windowHandler.SubmitSearchStringtoServer(searchKeyword, out jsonList);

            jsonList.Add(JObject.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + @"\sample01.json")));
            jsonList.Add(JObject.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + @"\sample02.json")));
            //for test

            ContentPanel.Children.Clear();
            var searchResult = windowHandler.GetSearchResults(jsonList);

            foreach(var item in searchResult)
            {
                DockPanel.SetDock(item, Dock.Top);
                ContentPanel.Children.Add(item);
            }
        }
    }
}
