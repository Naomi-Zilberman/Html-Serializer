using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace project2
{
    public class Selector
    {
        public string TagName;
        public string Id;
        public List<string> Classes;
        public Selector Parent;
        public Selector Child;

        public Selector()
        {
            Classes = new List<string>();
        }
        public void addChild(Selector child)
        {
            child.Parent = this;
            this.Child = child;
        }

        public override string ToString()
        {

            var classes = Classes != null ? string.Join(", ", Classes) : "אין מחלקות";
            return $"TagName: {TagName}, Id: {Id}, Classes: [{classes}], Parent: {Parent?.TagName}, Child:  {Child}";
        }

    }
}
