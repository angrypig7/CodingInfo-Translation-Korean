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

namespace WinAppClient
{
    partial class MainWindow : Window
    {

        public List<CategoriObj> GetCategoriObjs()
        {
            var categoriObjs = new List<CategoriObj>();
            var reader = new StreamReader(Directory.GetCurrentDirectory() + @"\uicontrol\categories.dat");

            for (; ; )
            {
                string readVal = reader.ReadLine();
                if (readVal == null) break;
                categoriObjs.Add(new CategoriObj(readVal));
            }
            reader.Close();
            return categoriObjs;
        }

        public List<SearchResult> GetSearchResults(JArray jObjectArray)
        {
            var result = new List<SearchResult>();

            foreach (var item in jObjectArray)
            {
                int jsonidx = int.Parse(item[0].ToString());
                string jsonTitle = item[1].ToString();
                string jsonAuthor = item[2].ToString();
                string jsonDate = item[7].ToString();
                string jsonTag = item[3].ToString();
                string jsonUrl = item[1].ToString();
                //TODO: json tag 파싱 마무리하기
                result.Add(new SearchResult(title: jsonTitle, author: jsonAuthor, date: jsonDate, tags: jsonTag, markdownDocLink: jsonUrl, idx: jsonidx));
            }
            return result;
        }

        private void SetSearchResultOutlook(SearchResult item)
        {
            item.MouseDoubleClick += SearchResult_MouseDoubleClick;
            item.Margin = new Thickness(0, 10, 0, 10);
            item.BorderBrush = Brushes.Black;
            item.BorderThickness = new Thickness(2);
        }

        private void AddSearchResulttoPanel(List<SearchResult> searchResult)
        {
            ContentPanel.Children.Clear();
            foreach (var item in searchResult)
            {
                DockPanel.SetDock(item, Dock.Top);
                SetSearchResultOutlook(item);
                ContentPanel.Children.Add(item);
            }
        }

        public void SubmitSearchStringtoServer(string targetString, out JArray jArray)
        {
            Uri uri = new Uri(URL + @"key=" + targetString);
            Console.WriteLine(uri.ToString());
            HttpWebRequest webRequest = HttpWebRequest.CreateHttp(uri);
            WebResponse webResponse;
            jArray = new JArray();

            webRequest.Method = "GET";
            webRequest.UserAgent = @"Chrome";
            webResponse = webRequest.GetResponse();
            Stream responseStream = webResponse.GetResponseStream();
            //검색 값을 서버에 보내고, 웹페이지에 표시되는 모든 내용을 StreamReader 형식으로 반환

            //StreamReaderDebugWrite(responseStreamReader); //for test

            using (StreamReader sr = new StreamReader(responseStream))
            {
                string readVal = "";
                readVal = sr.ReadToEnd();
                jArray = JArray.Parse(readVal);
                Console.Write(jArray.ToString());
            }
        }

        public void SubmitCategoryStringtoServer(string targetString, out JArray jArray)
        {
            Uri uri = new Uri(URL + @"key=" + targetString);
            HttpWebRequest webRequest = HttpWebRequest.CreateHttp(uri);
            WebResponse webResponse;
            jArray = new JArray();

            webRequest.Method = "GET";
            webRequest.UserAgent = @"Chrome";
            webResponse = webRequest.GetResponse();
            Stream responseStream = webResponse.GetResponseStream();
            StreamReader responseStreamReader = new StreamReader(responseStream);
            //검색 값을 서버에 보내고, 웹페이지에 표시되는 모든 내용을 StreamReader 형식으로 반환

            StreamReaderDebugWrite(responseStreamReader);
        }


        private static void StreamReaderDebugWrite(StreamReader streamReader)
        {
            for (; ; )
            {
                var tmp = streamReader.ReadLine();
                if (tmp == null) break;
                else Console.WriteLine(tmp);
            }
        }
    }
}
