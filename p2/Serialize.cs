using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using p2;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace p2
{
    public  class Serialize
    {
        public async Task<string> Load(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var html = await response.Content.ReadAsStringAsync();
            return html;
        }
        public HtmlElement BuildTree(string html)
        {

            string cleanHtml = html.Trim();
            List<string> htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Select(s => s.Trim()).Where(s => s.Length > 0).ToList();
  
            HtmlElement rootElement = new HtmlElement();
            HtmlElement currentElement = rootElement; // Initialize currentElement with rootElement

            foreach (string line in htmlLines)
            {
                string firstWord = line.Trim().Split((' '), StringSplitOptions.RemoveEmptyEntries)[0];

                if (firstWord == "/html")
                {
                    break;
                }
                else if (firstWord.StartsWith("/") && currentElement.Parent != null)
                {
                    currentElement = currentElement.Parent;
                }
                else if (HtmlHelper.Instance.HtmlVoidTags.Contains(firstWord) || line.EndsWith('/'))
                {
                    HtmlElement newElement = new HtmlElement
                    {
                        Name = firstWord,
                        Parent = currentElement
                    };


                    if (currentElement != null)
                    {
                        currentElement.Children.Add(newElement); 
                    }
                    ParseAttributes(line.Substring(firstWord.Length).Trim(), newElement);
            
                }
                else if (HtmlHelper.Instance.HtmlTags.Contains(firstWord))
                {
                    HtmlElement newElement = new HtmlElement
                    {
                        Name = firstWord,
                        Parent = currentElement
                    };
                    if (currentElement != null)
                    {
                        currentElement.Children.Add(newElement); 
                        currentElement = newElement;
                    }
                    ParseAttributes(line.Substring(firstWord.Length).Trim(), newElement);
                }

                else
                {
                    if (currentElement != null)
                    {
                        currentElement.InnerHtml = line.Trim();
                    }
                }
            }
            return rootElement;
        }
        void ParseAttributes(string attributeLine, HtmlElement element)
        {
            var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(attributeLine);
            foreach (Match attribute in attributes)
            {
                var attributeName = attribute.Groups[1].Value;
                var attributeValue = attribute.Groups[2].Value;

                if (attributeName == "class")
                {
                    element.Classes.AddRange(attributeValue.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                }
                else if (attributeName == "id")
                {
                    element.Id = attributeValue;
                }
                else
                {
                    element.Attributes.Add($"{attributeName}=\"{attributeValue}\"");
                }
            }
        }

 
    }
}
