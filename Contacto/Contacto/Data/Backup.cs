using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Contacto.Data
{
    class Backup
    {
        public string id
        { get; set; }

        public string title
        { get; set; }

        public string myContactFile
        { get; set; }
        public string myGroupFile
        { get; set; }
        public string date
        { get; set; }
    }
}
