using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using MagicCompound.Configs;
using MagicCompound.Merge;

namespace MagicCompound.Extensions
{
    public static partial class StringExtension
    {
        public static string Capitalize(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var match = LettersRegex().Match(input);
            if (!match.Success)
                return input;

            int index = match.Index;

            return input[..index] +
                char.ToUpper(input[index]) +
                input[(index + 1)..].ToLower();
        }

        public static string ReplacePathMarkers(this string value/*, string? targetPath = null*/)
        {
            //Config config = ConfigManager.GetConfig();
            return value.Replace("%ProgramFolder%", AppDirectories.BaseDirectory)
                        //.Replace("%AssetsFolder%", MergeDirectories.GetAssetsFolder(config))
                        .Replace("%ConfigsFolder%", AppDirectories.OutputDirectory)
                        .Replace("%OutputFolder%", AppDirectories.OutputDirectory)
                        //.Replace("%TargetFolder%", MergeDirectories.GetOutputFolder(config))
                        //.Replace("%Name%", Path.GetFileNameWithoutExtension(targetPath ?? MagicMerge.Target))
                        ;
        }

        [GeneratedRegex(@"\p{L}")]
        private static partial Regex LettersRegex();
    }
}
