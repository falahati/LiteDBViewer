using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LiteDBViewerVersionSelector
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                OpenDatabases(args);
            }
            else
            {
                var ofd = new OpenFileDialog
                {
                    CheckFileExists = true,
                    Multiselect = true,
                    RestoreDirectory = true,
                    Title = @"Open LiteDB Database File",
                    Filter = @"LiteDB Files|*.*"
                };
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                OpenDatabases(ofd.FileNames);
            }
        }

        private static void OpenDatabases(IEnumerable<string> fileNames)
        {
            var filesByVersion = new Dictionary<LiteDBVersion, List<string>>();
            foreach (var fileName in fileNames)
            {
                try
                {
                    var litedbVersion = VersionSelector.GetFileVersion(fileName);
                    if (litedbVersion == LiteDBVersion.Unknown)
                    {
                        throw new BadLiteDBVersionException();
                    }
                    if (!filesByVersion.ContainsKey(litedbVersion))
                    {
                        filesByVersion.Add(litedbVersion, new List<string>());
                    }
                    filesByVersion[litedbVersion].Add(fileName);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, fileName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            foreach (var filesVersionPair in filesByVersion)
            {
                try
                {
                    VersionSelector.OpenLiteDBViewer(filesVersionPair.Value.ToArray(), filesVersionPair.Key);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        string.Join(", ", filesVersionPair.Value.Select(fileName => $"'{fileName}'")),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}