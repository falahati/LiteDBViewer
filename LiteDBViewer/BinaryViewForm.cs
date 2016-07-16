using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LiteDB;

namespace LiteDBViewer
{
    internal partial class BinaryViewForm : Form
    {
        private readonly BsonValue _cell;

        public BinaryViewForm()
        {
            InitializeComponent();
        }

        public BinaryViewForm(BsonValue cell) : this()
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
            textBox.Visible = !(pictureBox.Visible = rb_image.Checked);
            if (rb_image.Checked)
            {
                try
                {
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Image = (Bitmap) ((new ImageConverter()).ConvertFrom(_cell.AsBinary));
                }
                catch
                {
                    pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox.Image = pictureBox.ErrorImage;
                }
            }
            else if (rb_ascii.Checked)
            {
                textBox.Text = Encoding.ASCII.GetString(_cell.AsBinary);
            }
            else if (rb_utf7.Checked)
            {
                textBox.Text = Encoding.UTF7.GetString(_cell.AsBinary);
            }
            else if (rb_utf32.Checked)
            {
                textBox.Text = Encoding.UTF32.GetString(_cell.AsBinary);
            }
            else if (rb_unicode.Checked)
            {
                textBox.Text = Encoding.Unicode.GetString(_cell.AsBinary);
            }
            else if (rb_utf8.Checked)
            {
                textBox.Text = Encoding.UTF8.GetString(_cell.AsBinary);
            }
            else
            {
                textBox.Text = 0.ToString("X8") + @"   ";
                var bytes = _cell.AsBinary;
                var chars = string.Empty;
                var l = 0;
                for (var i = 0; i < bytes.Length; i++)
                {
                    textBox.Text += bytes[i].ToString("X2");
                    chars += bytes[i] > 0x7F ? '.' : (char) bytes[i];
                    l = (i + 1)%16;
                    if (l == 0)
                    {
                        textBox.Text += @"  " + chars + Environment.NewLine + (i + 1).ToString("X8") + @"   ";
                        chars = string.Empty;
                    }
                    else if (l == 8)
                        textBox.Text += @"  ";
                    else
                        textBox.Text += ' ';
                }
                if (!string.IsNullOrWhiteSpace(chars))
                {
                    textBox.Text += new string(' ', 40 - l) + @"  " + chars;
                }
            }
        }
        
        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}