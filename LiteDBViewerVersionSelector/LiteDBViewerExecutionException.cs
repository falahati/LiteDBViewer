using System;

namespace LiteDBViewerVersionSelector
{
    public class LiteDBViewerExecutionException : Exception
    {
        public LiteDBViewerExecutionException(LiteDBVersion version, Exception innerException)
            : base(VersionToString(version), innerException)
        {
        }

        public LiteDBViewerExecutionException(LiteDBVersion version) : base(VersionToString(version))
        {
        }

        private static string VersionToString(LiteDBVersion version)
        {
            var versionString = "LiteDBViewer";
            switch (version)
            {
                case LiteDBVersion.LiteDB_0_9:
                case LiteDBVersion.LiteDB_1_0:
                    versionString = "LiteDBViewer v1.0";
                    break;
                case LiteDBVersion.LiteDB_2_0RC:
                    versionString = "LiteDBViewer v2.0RC2";
                    break;
                case LiteDBVersion.LiteDB_2_0:
                    versionString = "LiteDBViewer v2.0";
                    break;
                case LiteDBVersion.LiteDB_3_0:
                    versionString = "LiteDBViewer v3.0";
                    break;
                case LiteDBVersion.LiteDB_4_0:
                    versionString = "LiteDBViewer v4.0";
                    break;
            }
            return $"Can not find or execute the {versionString}. Please make sure that you have it installed.";
        }
    }
}