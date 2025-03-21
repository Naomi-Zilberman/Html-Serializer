using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace project2
{
    public class HtmlElement
    {
        //מאפיינים
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        //Parent ו-Children שמאפשרים לי ליצור עץ של אוביקטים.
        public HtmlElement parent { get; set; }
        public List<HtmlElement> Children { get; set; }
        public HtmlElement(int id, string name, List<string> attributes = null, List<string> classes = null, string innerHtml = null)
        {
            id = id;
            Name = name;
            Attributes = attributes ?? new List<string>();
            Classes = classes ?? new List<string>();
            InnerHtml = innerHtml;
            Children = new List<HtmlElement>();



        }
        public HtmlElement()
        {
            Attributes = new List<string>();
            Classes = new List<string>();

            Children = new List<HtmlElement>();
        }
        public HtmlElement(string name)
        {
            this.Name = name;
            Attributes = new List<string>();
            Classes = new List<string>();

            Children = new List<HtmlElement>();
        }
        public void addChild(HtmlElement child)
        {
            child.parent = this;
            Children.Add(child);

        }

        public override string ToString()
        {
            return $"id:{Id},name:{Name},Attributes:[{string.Join(",", Attributes)}]" +
                $",class:[{string.Join(",", Classes)}],InnerHtml:[{string.Join(",", InnerHtml)}]" + $",children:[{string.Join(",%", Children)}]";

        }

        //פונקציה המחזירה את כל הדורות
        public static List<HtmlElement> Descendants(HtmlElement root)
        {
            Queue<HtmlElement> q1 = new Queue<HtmlElement>();
            List<HtmlElement> l1 = new List<HtmlElement>();
            HtmlHelper helper = new HtmlHelper();
            HtmlElement h;
            q1.Enqueue(root);

            while (q1.Count > 0)
            {
                h = q1.Dequeue();
                l1.Add(h);
                foreach (var element in h.Children)
                {
                    q1.Enqueue(element);
                }
            }
            return l1;
        }
        //IEnumerable-ממשק שמאפשר לרוץ על אוסף
        //yield-נחזיר ערך רק כאשר מבקשים אותו. כל פעם את הערך הנוכחי
        //PROGRAM-עשיתי FOREACH -שרק ככה יכולה לעבוד
        //פונקציה שמדפיסה את האבא
        public static IEnumerable<HtmlElement> Ancestors(HtmlElement child)
        {
            List<HtmlElement> l1 = new List<HtmlElement>();
            l1.Add(child);
            while (child.parent != null)
            {
                l1.Add(child.parent);
                child = child.parent;
            }
            yield return l1[l1.Count - 1];
        }

        public static List<HtmlElement> end(HtmlElement h, Selector rootS)
        {

            List<HtmlElement> l = new List<HtmlElement>();
            end(h, rootS, l);
            return l;

        }

        //פונקציה המחזירה רשימה של אוביקטים העונים לתנאים שבעץ ה selector
        public static void end(HtmlElement h, Selector rootS, List<HtmlElement> l)
        {
            Selector s = rootS;
            HtmlElement hh = h;
            if (h == null)
                return;
            while (rootS != null)
            {
                bool n = false, cl = false, i = false, cl1 = false;
                if (rootS.TagName == null || rootS.TagName.Equals(h.Name))
                { n = true; }

                if (rootS.Id == null || h.Id == rootS.Id)
                { i = true; }

                if (rootS.Classes.Count == 0)
                { cl = true; }
                else
                    foreach (var c in rootS.Classes)
                    {
                        if (!h.Classes.Contains(c))
                        { cl1 = true; }
                    }
                if (cl1 == false)
                { cl = true; }

                if (n && cl && i)
                {
                    rootS = rootS.Child;
                    for (int j = 0; j < h.Children.Count; j++)
                        h = h.Children[j];
                }
                else
                    break;
            }
            if (rootS == null)
                l.Add(hh);
            for (int i = 0; i < hh.Children.Count; i++)
                end(hh.Children[i], s, l);
        }
    }
}




