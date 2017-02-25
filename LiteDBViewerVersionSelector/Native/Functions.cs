using System;
using System.Runtime.InteropServices;
using System.Text;

namespace LiteDBViewerVersionSelector.Native
{
    internal static class Functions
    {
        // By 'Ohad Schneider' and StackOverflow community
        // http://stackoverflow.com/a/17773402/1913051

        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern uint AssocQueryString(AssocF flags, AssocStr str,
            string pszAssoc, string pszExtra, [Out] StringBuilder pszOut, ref uint
                pcchOut);

        public static string AssocQueryString(AssocStr association, string extension)
        {
            const int S_OK = 0;
            const int S_FALSE = 1;

            uint length = 0;
            var ret = AssocQueryString(AssocF.None, association, extension, null, null, ref length);
            if (ret != S_FALSE)
            {
                throw new InvalidOperationException("Could not determine associated string");
            }

            var sb = new StringBuilder((int) length);
            ret = AssocQueryString(AssocF.None, association, extension, null, sb, ref length);
            if (ret != S_OK)
            {
                throw new InvalidOperationException("Could not determine associated string");
            }

            return sb.ToString();
        }
    }
}