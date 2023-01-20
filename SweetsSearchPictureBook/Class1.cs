using Amazon.Auth.AccessControlPolicy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;

namespace SweetsSearchPictureBook
{
    public class Rootobject
    {
        public Item[] item { get; set; }
        public string status { get; set; }
        public string count { get; set; }
    }

    
    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }
        //public Kana kana { get; set; }
        public object maker { get; set; }
        public object price { get; set; }

        //[Newtonsoft.Json.JsonIgnore]        
        public string type { get; set; }

        public string regist { get; set; }
        public string url { get; set; }
        public Tags tags { get; set; }
        public string image { get; set; }
        public string comment { get; set; }
        public string area { get; set; }
    }

    //public class Kana
    //{
    //    public object kana { get; set; }
    //}

    public class Tags
    {
        public object tag { get; set; }
    }


}
