using System;
using System.Collections.Generic;

namespace ASS.RandomUserGenerator.Generators
{
    class ListsOfNames
    {
        public IEnumerable<string> Male { get; set; }
        public IEnumerable<string> Female { get; set; }
        public IEnumerable<string> Last { get; set; }

        public ListsOfNames()
        {
            Male = new List<string>();
            Female = new List<string>();
            Last = new List<string>();
        }
    }
}
