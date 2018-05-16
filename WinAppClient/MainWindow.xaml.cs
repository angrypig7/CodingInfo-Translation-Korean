using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Threading;
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
        string URL;
        BackgroundWorker BG_Searcher;

        public MainWindow()
        {
            InitializeComponent();
            URL = WinAppClient.Properties.Settings.Default.WebURL;
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
            foreach(var item in this.GetCategoriObjs())
            {
                item.MouseDoubleClick += CategoriObj_MouseDoubleClick;
                CategoryBar.Items.Add(item);
            }
        }

        private void CategoriObj_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var jsonArray = new JArray();
            string categoryVal = "category ";
            categoryVal += (sender as CategoriObj).CategoryName;
            SubmitCategoryStringtoServer(categoryVal, out jsonArray);
            //TODO: 카테고리 정보 전송 구현
            //AddSearchResulttoPanel(searchResult);
        }

        private void TB_Search_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_Search.Text = null;
            TB_Search.Foreground = Brushes.Black;
        }

        private void BT_Search_Submit_Click(object sender, RoutedEventArgs e)
        {
            var jsonArray = new JArray();
            string searchKeyword = TB_Search.Text;

            TB_Search.Text = null;
            this.SubmitSearchStringtoServer(searchKeyword, out jsonArray);

            var searchResult = this.GetSearchResults(JArray.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + @"\sample02.json")));
            //for test

            AddSearchResulttoPanel(searchResult);
        }

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
