<<<<<<< HEAD
﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WinAppClient
{
    class MainWindowHandler
    {
        public MainWindowHandler()
        {
        }

        public List<CategoriObj> GetCategoriObjs()
        {
            var categoriObjs = new List<CategoriObj>();
            var reader = new StreamReader(Directory.GetCurrentDirectory() + @"\uicontrol\categories.txt");

            for(; ; )
            {
                string readVal = reader.ReadLine();
                if (readVal == null) break;
                categoriObjs.Add(new CategoriObj(readVal));
            }

            return categoriObjs;
        }

        public List<JObject> GetJsonList()
        {
            var resultList = new List<JObject>();
            //TODO: 서버와 통신해서 json 리스트 생성하기
            return resultList;
        }

        public void AddSearchResults(DockPanel targetPanel, List<JObject> jObjectsList)
        {
            targetPanel.Children.Clear();

            foreach(var item in jObjectsList)
            {
                var result = new SearchResult(title: item["title"].ToString(), content: item["content"].ToString(), 
                    author: item["author"].ToString(), date: item["date"].ToString(), tag: item["tag"].ToString());

                DockPanel.SetDock(result, Dock.Top);
                targetPanel.Children.Add(result);
            }
        }
    }
}
=======
﻿using System;
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
    partial class MainWindow : Window
    {

        public List<CategoriObj> GetCategoriObjs()
        {
            var categoriObjs = new List<CategoriObj>();
            var reader = new StreamReader(Directory.GetCurrentDirectory() + @"\uicontrol\categories.dat");

            for(; ; )
            {
                string readVal = reader.ReadLine();
                if (readVal == null) break;
                categoriObjs.Add(new CategoriObj(readVal));
            }
            return categoriObjs;
        }

        public List<SearchResult> GetSearchResults(JArray jObjectArray)
        {
            var result = new List<SearchResult>();

            foreach(var item in jObjectArray)
            {
                string jsonTitle = item["title"].ToString();
                string jsonAuthor = item["author"].ToString();
                string jsonDate = item["date"].ToString();
                string jsonTag = item["tag"].ToString();
                string jsonUrl = item["url"].ToString();
                //TODO: json tag 파싱 마무리하기
                result.Add(new SearchResult(title: jsonTitle, author: jsonAuthor, date: jsonDate, tags: jsonTag, markdownDocLink:jsonUrl));
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
            Uri uri = new Uri(URL + @"?key=" + targetString);
            HttpWebRequest webRequest = HttpWebRequest.CreateHttp(uri);
            WebResponse webResponse;
            jArray = new JArray();

            webRequest.Method = "GET";
            webRequest.UserAgent = @"Chrome";
            webResponse = webRequest.GetResponse();
            Stream responseStream = webResponse.GetResponseStream();
            StreamReader responseStreamReader = new StreamReader(responseStream);
            StreamReaderTest(responseStreamReader);
        }

        public void SubmitCategoryStringtoServer(string targetString, out JArray jArray)
        {
            Uri uri = new Uri(URL + @"?key=" + targetString);
            HttpWebRequest webRequest = HttpWebRequest.CreateHttp(uri);
            WebResponse webResponse;
            jArray = new JArray();

            webRequest.Method = "GET";
            webRequest.UserAgent = @"Chrome";
            webResponse = webRequest.GetResponse();
            Stream responseStream = webResponse.GetResponseStream();
            StreamReader responseStreamReader = new StreamReader(responseStream);
            StreamReaderTest(responseStreamReader);
        }


        private static void StreamReaderTest(StreamReader streamReader)
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
>>>>>>> 2d8bc8f21be54ead0196e2c28ffb213e3d3eed61
