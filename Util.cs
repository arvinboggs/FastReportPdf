using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FastReportPdf
{
    internal class Util
    {
        // https://stackoverflow.com/a/66285728/334045
        private static string? pCachedValue = null;
        public static string AppDirectory => pCachedValue ??= calculatePath();

        private static string calculatePath()
        {
            string pathName = GetSourceFilePathName();
            pathName = System.IO.Path.GetDirectoryName(pathName);

            var pBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (pBaseDirectory.StartsWith(pathName))
                return pathName;
            else
                return pBaseDirectory;
        }
        public static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) //
            => callerFilePath ?? "";
    }
}
