using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using JsonPrettyPrinterPlus;
using JsonPrettyPrinterPlus.JsonPrettyPrinterInternals;
using LiteDB;

namespace LiteDBViewer
{
    internal partial class StringViewForm : Form
    {
        private readonly string _string;

        private BsonDocument _dataRowValue;

        public StringViewForm()
        {
            InitializeComponent();
        }

        public StringViewForm(string value, bool json = false, BsonDocument dataRowValue = null) : this()
        {
            _dataRowValue = dataRowValue;
            if (_dataRowValue != null)
            {
                textBox.ReadOnly = false;
                btn_upd_value.Enabled = true;
                btn_upd_value.Visible = true;
            }
            else
            {
                textBox.ReadOnly = true;
                btn_upd_value.Enabled = false;
                btn_upd_value.Visible = false;
            }

            _string = value;
            if (json)
            {
                rb_Json.Checked = true;
            }
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

        private void btn_upd_value_Click(object sender, EventArgs e)
        {
            var mainForm = this.Owner as MainForm;
            if (mainForm != null)
            {
                _dataRowValue[mainForm._currentColumnName] = textBox.Text;
                mainForm.Database.GetCollection(mainForm._currentCollectionName).Update(_dataRowValue.AsDocument);
                mainForm.CallListBoxSelectedIndexChange();
                MessageBox.Show($"The value for cell \"{mainForm._currentColumnName}\" has been updated.", "Save Cell Changes Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("Operation cannot be performed.", "Save Cell Changes Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}