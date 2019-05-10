using OpenCCureJournalViewExcel.Model.Database;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCCureJournalViewExcel.Model.Objects
{
    class JournalViewList
    {
        public string Location { get; set; }
        
        public List<JournalView> GetJournalViews()
        {
            string commandString = string.Format("SELECT * FROM [{0}] ORDER BY [{1}] DESC, [{2}] DESC", CJournalViews.TableName, CJournalViews.ServerDateTime, CJournalViews.MessageDateTime);
            List<JournalView> journalViews = new List<JournalView>();
            using (OleDbConnection con = ConnectionString.NewConnection(Location))
            {
                OleDbCommand command = new OleDbCommand(commandString, con);
                con.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    journalViews.Add(new JournalView
                    {
                        MessageType = reader[CJournalViews.MessageType].ToString(),
                        ServerDateTime = DateTime.Parse(reader[CJournalViews.ServerDateTime].ToString()),
                        MessageDateTime = DateTime.Parse(reader[CJournalViews.MessageDateTime].ToString()),
                        Object1Name = reader[CJournalViews.Object1Name].ToString(),
                        MessageText = reader[CJournalViews.MessageText].ToString()
                    });
                }
                con.Close();
            }
            return journalViews.OrderBy(item => item.ServerDateTime).ToList();
        }

        public List<JournalView> GetJournalViewsByMessageType(string messageType) => GetJournalViews().FindAll(item => item.MessageType.Equals(messageType));

        public List<string> GetMessageTypes()
        {
            string commandString = string.Format("SELECT [{0}] FROM [{1}] GROUP BY [{0}]", CJournalViews.MessageType, CJournalViews.TableName);
            List<string> journalViews = new List<string>();
            using (OleDbConnection con = ConnectionString.NewConnection(Location))
            {
                OleDbCommand command = new OleDbCommand(commandString, con);
                con.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    journalViews.Add(reader[CJournalViews.MessageType].ToString());
                }
                con.Close();
            }
            return journalViews;
        }

        public List<string> GetObject1Names(string messageType = "")
        {
            string commandString = string.Format("SELECT [{0}], [{1}] FROM [{2}] GROUP BY [{0}],  [{1}]", CJournalViews.MessageType, CJournalViews.Object1Name, CJournalViews.TableName);
            List<string> journalViews = new List<string>();
            using (OleDbConnection con = ConnectionString.NewConnection(Location))
            {
                OleDbCommand command = new OleDbCommand(commandString, con);
                con.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(messageType))
                    {
                        journalViews.Add(reader[CJournalViews.Object1Name].ToString());
                    }
                    else
                    {
                        if (reader[CJournalViews.MessageType].ToString() == messageType)
                        {
                            journalViews.Add(reader[CJournalViews.Object1Name].ToString());
                        }
                    }
                }
                con.Close();
            }
            return journalViews;
        }
    }
}
