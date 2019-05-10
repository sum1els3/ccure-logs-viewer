using OpenCCureJournalViewExcel.Model.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCCureJournalViewExcel.Presentation
{
    class Form1Presentation
    {
        public Form1Presentation(string location)
        {
            _journalViewList = new JournalViewList
            {
                Location = location
            };
        }

        private string _messageType = "Card Admitted";

        private JournalViewList _journalViewList;

        public List<string> GetMessageTypes => _journalViewList.GetMessageTypes();

        public List<string> GetObject1Names => _journalViewList.GetObject1Names(_messageType);

        public List<JournalView> GetJournalViewsByObject1(string object1) => _journalViewList.GetJournalViews().FindAll(item => item.Object1Name.Equals(object1) && item.MessageType.Equals(_messageType));

        public List<JournalView> GetJournalViewsByObject1(string object1, DateTime setDateTime) => _journalViewList.GetJournalViews().FindAll(item => item.Object1Name.Equals(object1) && item.MessageType.Equals(_messageType) && item.ServerDateTime.ToShortDateString() == setDateTime.ToShortDateString());

        public List<JournalView> GetJournalViewsByObject1(string object1, DateTime setDateTime, string shift)
        {
            List<JournalView> journalViews = new List<JournalView>();
            if (shift.Contains("Day"))
            {
                journalViews.AddRange(_journalViewList.GetJournalViews().FindAll(item => item.Object1Name.Equals(object1) && item.MessageType.Equals(_messageType) && item.ServerDateTime.ToShortDateString() == setDateTime.ToShortDateString()));
            }
            else if (shift.Contains("Night"))
            {
                journalViews.AddRange(_journalViewList.GetJournalViews().FindAll(item => item.Object1Name.Equals(object1) && item.MessageType.Equals(_messageType) && item.ServerDateTime.ToShortDateString() == setDateTime.ToShortDateString() && item.ServerDateTime.Hour >= 12));
                DateTime tomorrow = setDateTime.AddDays(1);
                journalViews.AddRange(_journalViewList.GetJournalViews().FindAll(item => item.Object1Name.Equals(object1) && item.MessageType.Equals(_messageType) && item.ServerDateTime.ToShortDateString() == tomorrow.ToShortDateString() && item.ServerDateTime.Hour <= 12));
            }
            return journalViews;
        }
    }
}
