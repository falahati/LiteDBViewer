using System;
using System.Windows.Forms;

namespace LiteDBViewer
{
    internal partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        public string EnteredPassword => txt_password.Text;

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            Activate();
            txt_password.Focus();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (EnteredPassword.Contains(";") || EnteredPassword.Contains("="))
            {
                MessageBox.Show(
                    @"Bad password string. Please make sure that there is no ';' or '=' character in your password.",
                    @"Database Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_password.Text))
            {
                txt_password.Text = string.Empty;
            }
            DialogResult = DialogResult.OK;
        }
    }
}