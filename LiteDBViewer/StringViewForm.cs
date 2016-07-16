using System;
using System.Windows.Forms;
using System.Xml.Linq;
using JsonPrettyPrinterPlus;
using JsonPrettyPrinterPlus.JsonPrettyPrinterInternals;
using LiteDB;

namespace LiteDBViewer
{
    internal partial class StringViewForm : Form
    {
        private readonly BsonValue _cell;

        public StringViewForm()
        {
            InitializeComponent();
        }

        public StringViewForm(BsonValue cell) : this()
        {
            _cell = cell;
            ShowData();
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton)?.Checked == true)
            {
                ShowData();
            }
        }

        private void ShowData()
        {
            textBox.Visible = !(webBrowser.Visible = rb_HTML.Checked);
            if (rb_HTML.Checked)
            {
                webBrowser.DocumentText = _cell.AsString;
            }
            else if (rb_Json.Checked)
            {
                try
                {
                    textBox.Text = new JsonPrettyPrinter(new JsonPPStrategyContext()).PrettyPrint(_cell.AsString);
                }
                catch (Exception)
                {
                    textBox.Text = _cell.AsString;
                }
            }
            else if (rb_XML.Checked)
            {
                try
                {
                    textBox.Text = XDocument.Parse(_cell.AsString).ToString();
                }
                catch (Exception)
                {
                    textBox.Text = _cell.AsString;
                }
            }
            else
            {
                textBox.Text = _cell.AsString;
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}