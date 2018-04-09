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

namespace WinAppClient.uicontrol
{
    /// <summary>
    /// ShowContent.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ShowContent : Window
    {
        string markdownObj;
        public string MarkdownObj { get => markdownObj; }

        public ShowContent()
        {
            InitializeComponent();
        }

        public ShowContent(string markdownString)
        {
            InitializeComponent();
            this.markdownObj = markdownString;
        }

        private void MarkdownViewer_Loaded(object sender, RoutedEventArgs e)
        {
            MarkdownViewer.NavigateToString(markdownObj);
        }
    }
}
