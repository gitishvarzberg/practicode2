using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace p2
{
    public class HtmlHelper
    {
       
        private readonly static HtmlHelper _instance=new HtmlHelper();
        public static HtmlHelper Instance => _instance;
        public string[] HtmlTags;
        public string[] HtmlVoidTags;

        private HtmlHelper()
        {
            string content1 = File.ReadAllText("HtmlTags.json");
            string content2 = File.ReadAllText("HtmlVoidTags.json");
            HtmlTags = JsonSerializer.Deserialize<string[]>(content1);
            HtmlVoidTags = JsonSerializer.Deserialize<string[]>(content2);

        }
    }
}
