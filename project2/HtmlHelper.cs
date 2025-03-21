using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace project2
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance => _instance;
        public string[] massageList { get; set; }
        public string[] HtmlTags { get; set; }
        public string[] HtmlVoidTags { get; set; }
        public HtmlHelper()
        {
            var contion = File.ReadAllText("seed/JSON Files/HtmlTags.json");
            var contion1 = File.ReadAllText("seed/JSON Files/HtmlVoidTags.json");
            HtmlTags = JsonConvert.DeserializeObject<string[]>(contion);
            HtmlVoidTags = JsonConvert.DeserializeObject<string[]>(contion1);

        }
    }
}
