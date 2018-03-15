using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
