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

namespace WinAppClient
{
    /// <summary>
    /// SearchResult.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SearchResult : UserControl
    {
        string title;
        string author;
        string date;
        string tags;

        public string Title { get => title; set => title = value; }
        public string Author { get => author; set => author = value; }
        public string Date { get => date; set => date = value; }
        public string Tags { get => tags; set => tags = value; }

        public SearchResult()
        {
            InitializeComponent();
        }

        public SearchResult(string title, string author, string date, string tag)
        {
            InitializeComponent();
            this.title = title;
            this.author = author;
            this.date = date;
            this.tags = tag;
        }

        private void MainControl_Loaded(object sender, RoutedEventArgs e)
        {
            TBX_Title.Text = title;
            TBX_Author.Text = author;
            TBX_Date.Text = date;
            TBX_Tag.Text = tags;
        }
    }
}
