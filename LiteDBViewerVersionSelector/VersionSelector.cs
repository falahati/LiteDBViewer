using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using LiteDBViewerVersionSelector.Native;

namespace LiteDBViewerVersionSelector
{
    public static class VersionSelector
    {
        public static LiteDBVersion GetFileVersion(string fileName)
        {
            if (DetectIs09(fileName))
            {
                return LiteDBVersion.LiteDB_0_9;
            }
            if (DetectIs10(fileName))
            {
                return LiteDBVersion.LiteDB_1_0;
            }
            if (DetectIs20RC(fileName))
            {
                return LiteDBVersion.LiteDB_2_0RC;
            }
            if (DetectIs20(fileName))
            {
                return LiteDBVersion.LiteDB_2_0;
            }
            if (DetectIs30(fileName))
            {
                return LiteDBVersion.LiteDB_3_0;
            }
            return LiteDBVersion.Unknown;
        }

        public static void OpenLiteDBViewer(string[] fileNames, LiteDBVersion version)
        {
            var classId = string.Empty;
            switch (version)
            {
                case LiteDBVersion.LiteDB_0_9:
                case LiteDBVersion.LiteDB_1_0:
                    classId = "litedbviewer.databasefile";
                    break;
                case LiteDBVersion.LiteDB_2_0RC:
                case LiteDBVersion.LiteDB_2_0:
                    classId = "litedbviewer2.databasefile";
                    break;
                case LiteDBVersion.LiteDB_3_0:
                    classId = "litedbviewer3.databasefile";
                    break;
                default:
                    throw new LiteDBViewerExecutionException(version);
            }
            try
            {
                var address = Functions.AssocQueryString(AssocStr.Executable, classId);
                Process.Start(new ProcessStartInfo
                {
                    FileName = address,
                    Arguments = string.Join(" ", fileNames.Select(fileName => $"\"{fileName}\"").ToArray()),
                    UseShellExecute = true
                });
            }
            catch (Exception exception)
            {
                throw new LiteDBViewerExecutionException(version, exception);
            }
        }

        private static bool DetectIs09(string fileName)
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

        private static bool DetectIs10(string fileName)
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

        private static bool DetectIs20RC(string fileName)
        {
            using (var s = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var header = new byte[4096];
                if (s.Length >= header.Length)
                {
                    s.Read(header, 0, header.Length);
                    return header[52] == 5; // FILE_VERSION
                }
            }
            return false;
        }

        private static bool DetectIs20(string fileName)
        {
            using (var s = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var header = new byte[4096];
                if (s.Length >= header.Length)
                {
                    s.Read(header, 0, header.Length);
                    return header[52] == 6; // FILE_VERSION
                }
            }
            return false;
        }

        private static bool DetectIs30(string fileName)
        {
            using (var s = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var header = new byte[4096];
                if (s.Length >= header.Length)
                {
                    s.Read(header, 0, header.Length);
                    return header[52] == 7; // FILE_VERSION
                }
            }
            return false;
        }
    }
}