using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using LiteDB;
using FileMode = System.IO.FileMode;

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
            if ((args != null) && (args.Length > 0))
            {
                forms.AddRange(args.Select(OpenDatabase).Where(form => form != null));
                if (forms.Count == 0)
                {
                    return;
                }
            }
            while (forms.Count == 0)
            {
                var ofd = new OpenFileDialog
                {
                    CheckFileExists = true,
                    Multiselect = true,
                    RestoreDirectory = true,
                    Title =
                        $@"Open LiteDB Database File - LiteDB Viewer v{
                            Assembly.GetExecutingAssembly().GetName().Version}",
                    Filter = $@"LiteDB v{Assembly.GetAssembly(typeof(LiteDatabase)).GetName().Version.Major} Files|*.*"
                };

                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                forms.AddRange(ofd.FileNames.Select(OpenDatabase).Where(form => form != null));
            }
            Application.Run(new MultiFormApplicationContext(forms.ToArray()));
        }

        private static Form OpenDatabase(string fileName)
        {
            return OpenDatabase(fileName, null);
        }

        private static Form OpenDatabase(string fileName, string password, bool upgrade = false)
        {
            try
            {
                return new MainForm(fileName, password, upgrade);
            }
            catch (LiteException liteException)
            {
                if (liteException.ErrorCode == LiteException.DATABASE_WRONG_PASSWORD)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show(liteException.Message, fileName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var passwordForm = new PasswordForm();
                    return passwordForm.ShowDialog() != DialogResult.OK
                        ? null
                        : OpenDatabase(fileName, passwordForm.EnteredPassword);
                }
                var moreInfo = string.Empty;
                switch (liteException.ErrorCode)
                {
                    case LiteException.INVALID_DATABASE:
                        if (DetectIs090(fileName))
                        {
                            moreInfo =
                                @"Databases created by LiteDB v0.9 are not supported for viewing. Consider upgrading the files.";
                        }
                        else if (DetectIs104(fileName))
                        {
                            moreInfo =
                                @"Databases created by LiteDB v1 are not supported for viewing. Consider upgrading the files.";
                        }
                        break;
                    case LiteException.INVALID_DATABASE_VERSION:
                        if (!upgrade)
                        {
                            moreInfo =
                                @"Databases created by LiteDB v2.0 are not supported for viewing. Consider upgrading the files. Should we try upgrading the file?";
                            if (
                                MessageBox.Show(moreInfo, fileName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                var newFileAddress = Path.ChangeExtension(fileName, ".v3" + Path.GetExtension(fileName));
                                try
                                {
                                    File.Copy(fileName, newFileAddress, false);
                                }
                                catch (Exception copyExceptionex)
                                {
                                    moreInfo = copyExceptionex.Message + Environment.NewLine +
                                               "Failed to copy the file prior to upgrading.";
                                    MessageBox.Show(moreInfo, fileName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return null;
                                }
                                return OpenDatabase(newFileAddress, password, true);
                            }
                        }
                        else
                        {
                            moreInfo =
                                @"Databases created by LiteDB v2.0 are not supported for viewing. Consider upgrading the files.";
                        }
                        break;
                }
                MessageBox.Show(
                    liteException.Message +
                    (string.IsNullOrEmpty(moreInfo) ? string.Empty : Environment.NewLine + moreInfo),
                    fileName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NotSupportedException notSupportedException)
            {
                MessageBox.Show(notSupportedException.Message, fileName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    @"Selected file is not a valid LiteDB Database file." + Environment.NewLine + exception.Message,
                    fileName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        private static bool DetectIs090(string fileName)
        {
            using (var s = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var header = new byte[4096];
                if (s.Length >= header.Length)
                {
                    s.Read(header, 0, header.Length);
                    return header[44] == 3; // FILE_VERSION
                }
            }
            return false;
        }

        private static bool DetectIs104(string fileName)
        {
            using (var s = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var header = new byte[4096];
                if (s.Length >= header.Length)
                {
                    s.Read(header, 0, header.Length);
                    return header[45] == 4; // FILE_VERSION
                }
            }
            return false;
        }
    }
}