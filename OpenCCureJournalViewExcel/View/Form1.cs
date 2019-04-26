using OpenCCureJournalViewExcel.Model;
using OpenCCureJournalViewExcel.Model.Objects;
using OpenCCureJournalViewExcel.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCCureJournalViewExcel.View
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _getDate = true;
        }

        public string Object1 => comboBox1.Text;
        public string Shift => comboBox2.Text;
        public DateTime SetDateTime => dateTimePicker1.Value;
        private bool _getDate;

        private Form1Presentation _form1Presentation;

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox2.Text = comboBox2.Items[0].ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _form1Presentation = new Form1Presentation(openFileDialog1.FileName);
                LoadObject1Names();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void LoadDataIntoDataGridView()
        {
            if (_form1Presentation != null)
            {
                dataGridView1.Rows.Clear();
                if (_getDate)
                {
                    foreach (JournalView journalView in _form1Presentation.GetJournalViewsByObject1(Object1, SetDateTime))
                    {
                        dataGridView1.Rows.Add
                            (
                                journalView.MessageType,
                                journalView.ServerDateTime,
                                journalView.MessageDateTime,
                                journalView.Object1Name,
                                journalView.MessageText
                            );
                    }
                }
                else
                {
                    foreach (JournalView journalView in _form1Presentation.GetJournalViewsByObject1(Object1))
                    {
                        dataGridView1.Rows.Add
                            (
                                journalView.MessageType,
                                journalView.ServerDateTime,
                                journalView.MessageDateTime,
                                journalView.Object1Name,
                                journalView.MessageText
                            );
                    }
                }
            }
            else
            {
                MessageBox.Show("No such log file loaded");
            }
        }

        private void LoadObject1Names()
        {
            comboBox1.Items.Clear();
            foreach (string object1 in _form1Presentation.GetObject1Names)
            {
                comboBox1.Items.Add(object1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _getDate = !_getDate;
            if (_getDate)
            {
                button2.Text = "Show Specific";
            }
            else
            {
                button2.Text = "Show All";
            }
            if (_form1Presentation != null)
            {
                LoadDataIntoDataGridView();
            }
        }
    }
}
