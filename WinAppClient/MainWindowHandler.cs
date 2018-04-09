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

        /*
        public void SubmitSearchStringtoServer(string targetString, out JArray)
        {
            //TODO: 서버에 submit 한 후 json 리스트 가져오기
        }
        */
    }
}
