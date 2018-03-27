using System;
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
