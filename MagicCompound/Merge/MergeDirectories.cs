using System.IO;
using MagicCompound.Configs;
using MagicCompound.Extensions;
using MagicCompound.Utils;
using SixLabors.ImageSharp;

namespace MagicCompound.Merge
{
    internal class MergeDirectories
    {
        internal static string? GetFileName(Config? config = null, string? targetlPath = null)
        {
            if (ConfigManager.IsLoaded())
            {
                Config configuration = ConfigManager.GetConfig(config);
                return configuration.OutputName?.Replace("%Name%",
                    Path.GetFileNameWithoutExtension(targetlPath ?? MagicMerge.Target));
            }
            else return null;
        }

        internal static string GetOutputFormat(Image targetImage, Config? config = null)
        {
            if (ConfigManager.IsLoaded())
            {
                Config configuration = ConfigManager.GetConfig(config);
                string staticFormat = configuration.OutputFormat?[0] ?? "jpg";
                string dynamicFormat = configuration.OutputFormat?[1] ?? "gif";
                return !ImageUtil.IsAnimatedImage(targetImage) ? staticFormat : dynamicFormat;
            }
            else return "jpg";
        }

        internal static string GetConfigFile(string[]? args)
        {
            string? config = Parameters.GetValue(args, "Config")?.ReplacePathMarkers();
            
            return config ?? AppDirectories.ConfigFilePath;
        }

        internal static string GetAssetsFolder(Config? config = null)
        {
            if (ConfigManager.IsLoaded())
            {
                Config configuration = ConfigManager.GetConfig(config);
                string? assetsEnvironmentKey = configuration.AssetsFolder;

                return GetDirectory(assetsEnvironmentKey, AppDirectories.AssetsDirectory);
            }
            else return AppDirectories.AssetsDirectory;
        }

        internal static string GetOutputFolder(Config? config = null)
        {
            if (ConfigManager.IsLoaded())
            {
                Config configuration = ConfigManager.GetConfig(config);
                string? outputEnvironmentKey = configuration.OutputFolder;

                return GetDirectory(outputEnvironmentKey, AppDirectories.OutputDirectory, GetAssetsFolder(configuration));
            }
            else return AppDirectories.OutputDirectory;
        }

        private static string GetDirectory(string? environmentKey, string baseDirectory, string? assetsDirectory = null)
        {
            return environmentKey switch
            {
                "%ProgramFolder%" => AppContext.BaseDirectory,
                "%OutputFolder%" => AppDirectories.OutputDirectory,
                "%AssetsFolder%" => assetsDirectory ?? AppDirectories.AssetsDirectory,
                "%TargetFolder%" => Path.GetDirectoryName(MagicMerge.Target),
                _ => environmentKey?.ReplacePathMarkers()  // Власна вказана тека
            } ?? baseDirectory;
        }
    }
}
