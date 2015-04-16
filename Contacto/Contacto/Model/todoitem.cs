using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacto.Model
{
    class todoitem
    {
        public todoitem(string a, string b)
        { Id = a;
        Text = b;
        Complete = true;
        }
            public string Id { get; set; }
            public string Text { get; set; }
            public bool Complete { get; set; }
    }
}
