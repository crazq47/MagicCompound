using System.IO;
using System.Windows;
using MagicCompound.Merge;
using MagicCompound.Utils;

namespace MagicCompound.Configs
{
    public static class Parameters
    {
        public static void GetArguments(string[]? args = null)
        {
            List<string> files = [];
            List<string> parameters = [];

            if (args != null && args.Length > 0)
            {
                foreach (string arg in args)
                {
                    if (IsFilePath(arg)) files.Add(arg);
                    else if (IsParameter(arg))
                        parameters.Add(arg);
                }

                if (files.Count < 1)
                {
                    ImageUtil.ShowImagesDialog();
                }

                Parse([.. parameters]);
                ImageUtil.MergeDroppedImages([.. files]);
            }
            else ImageUtil.ShowImagesDialog();
        }

        public static string? GetValue(string[]? args, string key)
        {
            if (args != null)
            {
                foreach (var arg in args)
                {
                    string[] values = arg.Split(MergeSyntax.Equal, 2);

                    if (values.Length == 2 && values[0].TrimStart(MergeSyntax.Ignore[0]) == key)
                    {
                        return values[1];
                    }
                }
            }

            return null;
        }

        public static Dictionary<string, string?> Parse(string[]? args)
        {
            var parameters = new Dictionary<string, string?>();

            if (args != null)
            {
                foreach (var arg in args)
                {
                    string[] values = arg.Split(MergeSyntax.Equal, 2);

                    if (values.Length == 2)
                    {
                        parameters[values[0].TrimStart(MergeSyntax.Ignore[0])] = values[1];
                    }
                }
            }

            return parameters;
        }

        /// <summary>
        /// Перевіряє, чи є аргумент шляхом до існуючого файлу.
        /// </summary>
        public static bool IsFilePath(string arg)
        {
            try
            {
                return File.Exists(arg);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Перевіряє, чи є аргумент параметром.
        /// </summary>
        public static bool IsParameter(string arg)
        {
            return arg.StartsWith(MergeSyntax.Ignore);
        }
    }
}
