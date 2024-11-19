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

                return configuration.AssetsFolder switch
                {
                    "%ProgramFolder%" => AppContext.BaseDirectory,
                    "%OutputFolder%" => AppDirectories.OutputDirectory,
                    "%AssetsFolder%" => AppDirectories.AssetsDirectory,
                    "%TargetFolder%" => Path.GetDirectoryName(MagicMerge.Target),
                    _ => configuration.AssetsFolder?.ReplacePathMarkers() // Власна вказана тека
                } ?? AppDirectories.AssetsDirectory;
            }
            else return AppDirectories.AssetsDirectory;
        }

        internal static string GetOutputFolder(Config? config = null)
        {
            if (ConfigManager.IsLoaded())
            {
                Config configuration = ConfigManager.GetConfig(config);

                return (configuration).OutputFolder switch
                {
                    "%ProgramFolder%" => AppContext.BaseDirectory,
                    "%OutputFolder%" => AppDirectories.OutputDirectory,
                    "%AssetsFolder%" => GetAssetsFolder(configuration),
                    "%TargetFolder%" => Path.GetDirectoryName(MagicMerge.Target),
                    _ => configuration.OutputFolder?.ReplacePathMarkers()  // Власна вказана тека
                } ?? AppDirectories.OutputDirectory;
            }
            else return AppDirectories.OutputDirectory;
        }
    }
}
