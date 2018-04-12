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
using Markdig;
using Markdig.SyntaxHighlighting;

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

        private void IMG_Logo_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\rsc\logo.jpg");
            logo.EndInit();

            IMG_Logo.Source = logo;
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
            var jsonArray = new JArray();

            TB_Search.Text = null;
            string searchKeyword = TB_Search.Text;
            //windowHandler.SubmitSearchStringtoServer(searchKeyword, out jsonArray);

            ContentPanel.Children.Clear();
            var searchResult = windowHandler.GetSearchResults(JArray.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + @"\sample02.json")));
            //for test
            foreach (var item in searchResult)
            {
                DockPanel.SetDock(item, Dock.Top);
                SetSearchResultOutlook(item);
                ContentPanel.Children.Add(item);
            }
        }

        private void SetSearchResultOutlook(SearchResult item)
        {
            item.MouseDoubleClick += SearchResult_MouseDoubleClick;
            item.Margin = new Thickness(0, 10, 0, 10);
            item.BorderBrush = Brushes.Black;
            item.BorderThickness = new Thickness(2);
        }

        //private void SearchResult_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    (sender as SearchResult).Foreground = Brushes.Blue;
        //}

        private void SearchResult_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //TODO: 마크다운 창 예쁘게 꾸미기

            var test = File.ReadAllText(Directory.GetCurrentDirectory() + @"\sample01.md");
            //for test
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var markdownObj = Markdown.ToHtml(test, pipeline);
            var showContent = new uicontrol.ShowContent(markdownObj);
            showContent.Show();
        }

    }
}
