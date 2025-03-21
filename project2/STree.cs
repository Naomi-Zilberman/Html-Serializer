using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project2
{
    public class STree
    {
        public Selector root { get; set; }
        public STree()
        {
            root = new Selector();
        }
        public override string ToString()
        {
            return root.ToString();
        }
        public static Selector CreateSelector(string Query)
        {
            string[] arr = Query.Split(" ");
            STree st = new STree();
            Selector s = st.root;
            HtmlHelper helper = new HtmlHelper();
            foreach (string str in arr)
            {
                // split
                char[] delimiters = new char[] { '#', '.' };
                string[] s1 = str.Split(delimiters);
              
                int p;
                for (int i = 0; i < s1.Length; i++)
                {
                    p = str.IndexOf(s1[i]);
                    //האם אתה תגית?
                    if (p == 0)
                    {
                        if (helper.HtmlTags.Contains(s1[0]) || helper.HtmlVoidTags.Contains(s1[0]))
                            s.TagName = s1[0];
                    }
                    //מיקום בstring מבולגן עם מילים וסימנים

                    else if (str[p - 1] == '#')
                        s.Id = s1[i];
                    else if (str[p - 1] == '.')
                        s.Classes.Add(s1[i]);
                    else
                         if (helper.HtmlTags.Contains(s1[i]) || helper.HtmlVoidTags.Contains(s1[i]))
                    {
                        s.TagName = s1[i];
                    }


                }
                Selector c = new Selector();
                s.addChild(c);
                s = c;
            }

            return st.root;

        }
    }
}



