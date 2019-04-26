using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCCureJournalViewExcel.Model.Objects
{

    class JournalView
    {
        public string MessageType { get; set; }
        public DateTime ServerDateTime { get; set; }
        public DateTime MessageDateTime { get; set; }
        public string Object1Name { get; set; }
        public string MessageText { get; set; }
    }
}
