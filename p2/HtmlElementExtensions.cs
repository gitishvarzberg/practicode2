using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace p2
{  
    public static class HtmlElementExtensions
    {
        public static List<HtmlElement> FindElements(this HtmlElement rootElement, Selector selector)
        {
            List<HtmlElement> result = new List<HtmlElement>();
            FindElementsRecursive(rootElement, selector, result);
            return result;
        }

        private static void FindElementsRecursive(HtmlElement treeElement, Selector currentSelector, List<HtmlElement> result)
        {
            List<HtmlElement> descendants = treeElement.Descendants().ToList();

            HashSet<HtmlElement> filteredDescendants = FilterDescendants(descendants, currentSelector);

            if (currentSelector.Child == null)
            {
                result.AddRange(filteredDescendants);
                return;
            }

            foreach (HtmlElement descendant in filteredDescendants)
            {
                FindElementsRecursive(descendant, currentSelector.Child, result);
            }
        }
        private static HashSet<HtmlElement> FilterDescendants(List<HtmlElement> descendants, Selector selector)
        {

            HashSet<HtmlElement> uniqueElements = new HashSet<HtmlElement>();

            foreach (HtmlElement element in descendants)
            {

                if (((selector.Id != null && selector.Id == element.Id) || (selector.Id == null)) &&
                    (selector.TagName == element.Name) &&
                    (selector.Classes.All(className => element.Classes.Contains(className))))
                {
                    uniqueElements.Add(element);
                }
            }

            return uniqueElements;
        }
    }
}
