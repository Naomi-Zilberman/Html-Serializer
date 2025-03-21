using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace project2
{
    public class Tree
    {
        public HtmlElement root { get; set; }

        public Tree()
        {
            root = new HtmlElement();
        }


        //לחלץ לנו איזשהו regex id class width למערך מסודר:  id=class=width ב מסודר
        //שנוכל להפוך אותם לאוביקט של אטריביוט
        //Matches-מחפשת לנו את כל הטענות שמתאים לביטוי הרגולרי הזה
        //כל התווים שבעולם חוץ מרווח
        static List<string> attirbut1(string line)
        {
            var atrbute = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line);
            return atrbute.Select(x => $"{x.Groups[1].Value}={x.Groups[2].Value}").ToList();

        }

        public override string ToString()
        {
            return root.ToString();
        }
        public void createTree(HtmlElement root, string html)
        {
            var cleanHtml = new Regex(@"(?<=^|>)[\s]+|[\s]+(?=<|$)").Replace(html, "");
            var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 0);

            HtmlHelper helper = new HtmlHelper();


            HtmlElement e = this.root;

            foreach (var line in htmlLines)
            {
                if (line == "html")
                {
                    e.Name = line;

                }
                else if (line[0] == '/' && e.parent != null)
                {
                    //עולים לאבא
                    e = e.parent;
                }

                else if (line == "/html")
                {
                    root = e;

                    //  Console.WriteLine("\n" + "\n" + root);
                    return;
                }


                //פותחת עם סיום
                else if (helper.HtmlTags.Contains(line))
                {
                    var newElement = new HtmlElement(line);
                    //e.Children.Add(newElement);
                    e.addChild(newElement);
                    //newElement.parent = e;
                    e = newElement;

                }
                //תחילית תגית
                else if (line.Contains(" ") && helper.HtmlTags.Contains(line.Substring(0, line.IndexOf(" "))))
                {
                    string n = line.Substring(0, line.IndexOf(" "));
                    var newElement = new HtmlElement(n);
                    e.addChild(newElement);

                    e = newElement;
                    // line = n;
                }
                //אם תגית פותחת ללא סיום
                else if (helper.HtmlVoidTags.Contains(line))
                {
                    e.Children.Add(new HtmlElement(line));
                }
                else
                {
                    e.InnerHtml = line;
                }
                //בדיקה עלAttributes 
                if (helper.HtmlTags.Contains(e.Name))
                {
                    if (line.Contains(" ") && e.Name == line.Substring(0, line.IndexOf(" ")))
                    {
                        e.Attributes = attirbut1(line.Substring(line.IndexOf(" ")));
                    }
                    else
                    { e.Attributes = attirbut1(line); }
                    if (e.Attributes.Count > 0)
                    {

                        foreach (var attr in e.Attributes)
                        {
                            if (attr.Contains("class"))
                            {
                                string t1 = attr.Substring(attr.IndexOf("class") + 6);

                                e.Classes = t1.Split(' ').ToList();
                            }

                            else if (attr.Contains("id"))
                            {
                                e.Id = attr.Substring(attr.IndexOf("id") + 3);
                            }

                        }

                    }

                }

            }
        }

    }
}