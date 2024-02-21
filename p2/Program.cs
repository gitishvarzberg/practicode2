
using p2;
using System.Text.RegularExpressions;
using System.Xml.Linq;

Serialize serialize = new Serialize();  

var html =await serialize.Load("https://learn.malkabruk.co.il/practicode/403/#_1");
var tree= serialize.BuildTree(html);

List<HtmlElement> result1 = tree.FindElements(Selector.ParseSelector("span")).ToList();
result1.ForEach(e => Console.WriteLine(e.ToString()));
Console.WriteLine(result1.Count());

List<HtmlElement> result2 = tree.FindElements(Selector.ParseSelector("a span")).ToList();
result2.ForEach(e => Console.WriteLine(e.ToString()));
Console.WriteLine(result2.Count());

List<HtmlElement> result3 = tree.FindElements(Selector.ParseSelector("a span.md-ellipsis")).ToList();
result3.ForEach(e => Console.WriteLine(e.ToString()));
Console.WriteLine(result3.Count());




