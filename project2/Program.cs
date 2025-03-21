//using project2;
using project2;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;


//HtmlHelper helper = new HtmlHelper();

//var html = await Load("https://hebrewbooks.org/beis");
var html = "<html>\r\n<head>\r\n<link rel=\"styleSheet\" href=\"../HTML/CSS/hom.css\">\r\n</head>\r\n<body>\r\n" +
   "<div id=\"white\" class=\"my_class_1 my_class_2\">\r\n<h1>אני עיפה</h1>\r\n<br>\r\n<p id=\"white\">ברצוני לעשות משהו יפה.</p>\r\n</div>\r\n</body>\r\n</html>";




async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    html = html.Substring(html.IndexOf("html") + 5);

    return html;
}

//זימון הפונקציה ליצירת עץ אלמנטים
Tree t = new Tree();
t.createTree(t.root, html);
Console.WriteLine("--element tree--");
Console.WriteLine(t.root);
//STree.CreateSelector("div#myDive.classA.classB p#myDive.classC.classD");
Console.WriteLine("--selector tree--");
//עץ סלקטור
Selector s = STree.CreateSelector("div#white.my_class_1 h1");
Selector ss1 = STree.CreateSelector("#white");
//var ss =new Selector();
//ss = (Selector)s;
//Console.WriteLine(s);
Console.WriteLine(s.ToString());
Console.WriteLine("\n\n");    
Console.WriteLine("--Descendants--");
List<HtmlElement> list = HtmlElement.Descendants(t.root);
string s2 = string.Join("******************", list);
Console.WriteLine(s2);
Console.WriteLine("\n\n");
Console.WriteLine("------Ancestors----");
var ss = HtmlElement.Ancestors(list[list.Count - 1]);
foreach (var el in ss)
{
    Console.WriteLine(el.ToString());
}
Console.WriteLine("\n\n");
Console.WriteLine("--------------end------------");
List<HtmlElement> ll = HtmlElement.end(t.root, s);
string llS = string.Join("\n", ll);
Console.WriteLine(llS);

Console.WriteLine("\n\n");
Console.WriteLine("--------------end1------------");
List<HtmlElement> lls = HtmlElement.end(t.root, ss1);
string llSs = string.Join("\n", lls);
Console.WriteLine(llSs);

Console.WriteLine("\n\n");
Console.WriteLine("-----------print before sort------------");
ll.Add(ll[0]);
llS = string.Join("\n", ll);
Console.WriteLine(llS);
Console.WriteLine("\n\n");
////hashet
Console.WriteLine("-----------hashet--------------------");
var set = new HashSet<HtmlElement>();
foreach (var el in ll)
{
    set.Add(el);
}

set.ToList().ForEach(el => Console.WriteLine(el.ToString()));
