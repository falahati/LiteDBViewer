using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using JsonPrettyPrinterPlus;
using JsonPrettyPrinterPlus.JsonPrettyPrinterInternals;

namespace LiteDBViewer
{
    internal partial class StringViewForm : Form
    {
        private readonly string _string;

        public StringViewForm()
        {
            InitializeComponent();
        }

        public StringViewForm(string value) : this()
        {
            _string = value;
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
            textBox.Visible = !(webBrowserPanel.Visible = webBrowser.Visible = rb_HTML.Checked);
            if (rb_HTML.Checked)
            {
                webBrowser.DocumentText = _string;
            }
            else if (rb_Json.Checked)
            {
                try
                {
                    textBox.Text = new JsonPrettyPrinter(new JsonPPStrategyContext()).PrettyPrint(_string);
                }
                catch (Exception)
                {
                    textBox.Text = _string;
                }
            }
            else if (rb_XML.Checked)
            {
                try
                {
                    textBox.Text = XDocument.Parse(_string).ToString();
                }
                catch (Exception)
                {
                    textBox.Text = _string;
                }
            }
            else if (rb_base64.Checked)
            {
                textBox.Text = string.Empty;
                try
                {
                    new BinaryViewForm(Convert.FromBase64String(_string)).ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                rb_base64.Checked = false;
                rb_String.Checked = true;
            }
            else
            {
                textBox.Text = _string;
            }
            textBox.SelectionStart = 0;
            textBox.SelectionLength = 0;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Dump_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                RestoreDirectory = true,
                Title = @"Save string to file",
                Filter = @"Text|*.txt|All Files|*.*"
            };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                using (var writer = File.CreateText(sfd.FileName))
                {
                    writer.Write(_string);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Saving String", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StringViewForm_Shown(object sender, EventArgs e)
        {
            Enabled = false;
            ShowData();
            Enabled = true;
        }
    }
}