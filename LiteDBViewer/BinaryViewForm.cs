using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LiteDBViewer
{
    internal partial class BinaryViewForm : Form
    {
        private readonly byte[] _bytes;

        public BinaryViewForm()
        {
            InitializeComponent();
        }

        public BinaryViewForm(byte[] bytes) : this()
        {
            _bytes = bytes;
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
            textBox.Visible = !(pictureBox.Visible = rb_image.Checked);
            btn_asString.Enabled = !rb_hex.Checked && !rb_image.Checked;
            if (rb_image.Checked)
            {
                try
                {
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Image = (Bitmap) ((new ImageConverter()).ConvertFrom(_bytes));
                }
                catch
                {
                    pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox.Image = pictureBox.ErrorImage;
                }
            }
            else if (rb_ascii.Checked)
            {
                textBox.Text = Encoding.ASCII.GetString(_bytes);
            }
            else if (rb_utf7.Checked)
            {
                textBox.Text = Encoding.UTF7.GetString(_bytes);
            }
            else if (rb_utf32.Checked)
            {
                textBox.Text = Encoding.UTF32.GetString(_bytes);
            }
            else if (rb_unicode.Checked)
            {
                textBox.Text = Encoding.Unicode.GetString(_bytes);
            }
            else if (rb_utf8.Checked)
            {
                textBox.Text = Encoding.UTF8.GetString(_bytes);
            }
            else
            {
                var text = new StringBuilder();
                text.Append(0.ToString("X8") + @"   ");
                var chars = string.Empty;
                var l = 0;
                for (var i = 0; i < _bytes.Length; i++)
                {
                    chars += _bytes[i] < 0x20 || _bytes[i] > 0x7F ? '.' : (char) _bytes[i];
                    l = (i + 1)%16;
                    switch (l)
                    {
                        case 0:
                            text.Append(_bytes[i].ToString("X2") +
                                        $"  {chars}{Environment.NewLine}{(i + 1).ToString("X8")}   ");
                            chars = string.Empty;
                            break;
                        case 8:
                            text.Append(_bytes[i].ToString("X2") + @"  ");
                            break;
                        default:
                            text.Append(_bytes[i].ToString("X2") + ' ');
                            break;
                    }
                }
                if (!string.IsNullOrWhiteSpace(chars))
                {
                    text.Append(new string(' ', 40 - l) + @"  " + chars);
                }
                textBox.Text = text.ToString();
            }

            textBox.SelectionStart = 0;
            textBox.SelectionLength = 0;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AsString_Click(object sender, EventArgs e)
        {
            new StringViewForm(textBox.Text).ShowDialog();
        }

        private void Dump_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                RestoreDirectory = true,
                Title = @"Dump binary data to file",
                Filter = @"Binary|*.bin|All Files|*.*"
            };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                using (var writer = File.Create(sfd.FileName))
                {
                    writer.Write(_bytes, 0, _bytes.Length);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Dumping Binary", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BinaryViewForm_Shown(object sender, EventArgs e)
        {
            Enabled = false;
            ShowData();
            Enabled = true;
        }
    }
}