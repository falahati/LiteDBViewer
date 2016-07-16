using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LiteDBViewer
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var forms = new List<Form>();
            if (args != null)
            {
                foreach (var arg in args)
                {
                    try
                    {
                        forms.Add(new MainForm(arg));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, arg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            while (forms.Count == 0)
            {
                var ofd = new OpenFileDialog
                {
                    CheckFileExists = true,
                    Multiselect = true,
                    RestoreDirectory = true,
                    Title = @"Open LiteDB Data Storage File"
                };
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                foreach (var fileName in ofd.FileNames)
                {
                    try
                    {
                        forms.Add(new MainForm(fileName));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, fileName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            foreach (var form in forms)
            {
                form.Show();
                form.Activate();
            }
            Application.Run(new MultiFormApplicationContext(forms.ToArray()));
        }
    }
}