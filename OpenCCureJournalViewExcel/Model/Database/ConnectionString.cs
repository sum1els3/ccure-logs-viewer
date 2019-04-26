using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCCureJournalViewExcel.Model.Database
{
    internal static class ConnectionString
    {
        internal static OleDbConnection NewConnection(string location)
        {
            return new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"", location));
        }
    }
}
