using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2
{
    using System;

    public class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; } = new List<string>();
        public Selector Parent { get; set; }
        public Selector Child { get; set; } 

        //Static function to convert a query string to a Selector object
        public static Selector ParseSelector(string queryString)
        {

            // Split the query string into parts based on spaces
            string[] selectorParts = queryString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            Selector rootSelector = null;
            Selector currentSelector = null;
            foreach (string part in selectorParts)
            {
                Selector newSelector = new Selector();
                char[] separators = { '#', '.' };
                string[] selectorComponents = SplitStringPreserveCharacters(part, separators);

                foreach (string component in selectorComponents)
                {
                    if (component.StartsWith("#"))
                    {
                        // Update the Id property
                        newSelector.Id = component.Substring(1);
                    }
                    else if (component.StartsWith("."))
                    {
                        // Add to the Classes list
                        newSelector.Classes.Add(component.Substring(1));
                    }
                    else
                    {
                        // Check if it is a valid HTML tag name
                        if (IsValidTagName(component))
                        {
                  
                            newSelector.TagName= component;    
                        }
                        else
                        {
                            // Handle the case where the component is not a valid tag name (optional)
                            Console.WriteLine($"Invalid tag name: {component}");
                        }
                    }
                 }
                if (rootSelector == null)
                {
                    rootSelector = newSelector; 
                    currentSelector = newSelector;
                }
                else {
                    newSelector.Parent = currentSelector;
                    currentSelector.Child = newSelector;
                }
                currentSelector = newSelector;
            }

            return rootSelector;
        }

        // Helper function to check if a string is a valid HTML tag name
        private static bool IsValidTagName(string tagName)
        {
            if (HtmlHelper.Instance.HtmlTags.Contains(tagName)||HtmlHelper.Instance.HtmlVoidTags.Contains(tagName))
            {
                return true;
            }
            return false;   
        }

        static string[] SplitStringPreserveCharacters(string input, char[] separators)
        {
            List<string> parts = new List<string>();
            int startIndex = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (separators.Contains(input[i]))
                {
                    // Found a separator
                    string part = input.Substring(startIndex, i - startIndex);
                    parts.Add(part);
                    startIndex = i;
                }
            }

            // Add the remaining part after the last separator
            if (startIndex < input.Length)
            {
                string part = input.Substring(startIndex);
                parts.Add(part);
            }

            return parts.ToArray();
        }

        public void DisplayHierarchy(int indentation = 0)
        {
            Console.WriteLine(new string(' ', indentation) + this.ToString());
            Child?.DisplayHierarchy(indentation + 2);
        }

        public override string ToString()
        {
            return $"TagName: {TagName}, Id: {Id}, Classes: [{string.Join(", ", Classes)}]";
        }

    }



}
