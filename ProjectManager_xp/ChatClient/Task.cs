using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public class Task
    {
        public int id { get; set; }

        public string name { get; set; }

        public string project { get; set; }

        public string developer { get; set; }

        public int complexity { get; set; }

        public string deadline { get; set; }

        public string status { get; set; }

        public string creation_date { get; set; }
    }
}
