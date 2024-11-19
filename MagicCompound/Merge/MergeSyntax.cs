using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCompound.Merge
{
    public class MergeSyntax
    {
        public static char Key => '$';
        public static char Index => '!';
        public static char Equal => '=';
        public static string Ignore => "--";
        public static string Watermark => "watermark";
        public static string Target => "target";

        public static readonly Dictionary<string, string> Environments = new()
        {
            { "%ProgramFolder%", AppContext.BaseDirectory },
            { "%OutputFolder%", AppDirectories.OutputDirectory },
            { "%AssetsFolder%", AppDirectories.AssetsDirectory },
            { "%TargetFolder%", Path.GetDirectoryName(MagicMerge.Target) ?? string.Empty }
        };

        public static string TrunEnvironmentKey(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                return $"%{value.Replace(" ", string.Empty)}%";
            return value;
        }
    }
}
