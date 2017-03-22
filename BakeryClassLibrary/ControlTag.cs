using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryClassLibrary
{
    public class ControlTag
    {
        private string tag1;
        private string tag2;
        private string tag3;

        public ControlTag(string pTag1, string pTag2, string pTag3)
        {
            tag1 = pTag1;
            tag2 = pTag2;
            tag3 = pTag3;
        }
        public ControlTag(string pTag1, string pTag2)
        {
            tag1 = pTag1;
            tag2 = pTag2;
        }
        public ControlTag(string pTag1)
        {
            tag1 = pTag1;
        }

        public string getTag1()
        {
            return tag1;
        }
        public string getTag2()
        {
            return tag2;
        }
        public string getTag3()
        {
            return tag3;
        }

    }
}
