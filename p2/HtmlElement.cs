using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace p2
{
    public class HtmlElement
    {
        public string Id;
        public string Name;
        public List<string>Attributes;
        public List<string> Classes;
        public string InnerHtml;
        public HtmlElement Parent;
        public List<HtmlElement> Children;
        public HtmlElement()
        {
                Attributes = new List<string>();
                Classes = new List<string>();
                Children = new List<HtmlElement>();

        }
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {

                HtmlElement currentElement = queue.Dequeue();

                yield return currentElement;

                foreach (HtmlElement child in currentElement.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement current = this; 

            while (current.Parent != null)
            {
                current = current.Parent;
                yield return current;
            }
        }

        public override string ToString()
        {
            string str = " ";
            foreach (var attr in Attributes)
            {
                str += attr;
            }
            string str1 = " ";
            foreach (var attr in Classes)
            {
                str1 += attr;
            }
            return  "name: " + Name + " id: " + Id + "atri: " + str + "class: " + str1 +"parent: "+Parent.Name;
        }
    }

    //class Program
    //{
    //    static void Main()
    //    {
    //        // Example usage:
    //        HtmlElement root = BuildHtmlTree(); // Assume you have a method to build the HTML tree

    //        // Using Descendants with yield return
    //        foreach (var descendant in root.Descendants())
    //        {
    //            Console.WriteLine(descendant); // Use the appropriate way to display HtmlElement
    //        }
    //    }

    //    // Assume you have a method to build the HTML tree
    //    static HtmlElement BuildHtmlTree()
    //    {
    //        // Implement the logic to build the HTML tree
    //        // This is just a placeholder, replace it with your actual implementation
    //        return new HtmlElement();
    //    }
    //}



}
 

