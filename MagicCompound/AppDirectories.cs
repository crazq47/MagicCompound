using System.IO;
using MagicCompound.Configs;
using MagicCompound.Extensions;
using MagicCompound.Merge;

namespace MagicCompound
{
    public class AppDirectories
    {
        public static string BaseDirectory => AppDomain.CurrentDomain.BaseDirectory;
        public static string AssetsDirectory => Path.Combine(BaseDirectory, "Assets");
        public static string OutputDirectory => Path.Combine(BaseDirectory, "Output");
        public static string ConfigsDirectory => Path.Combine(BaseDirectory, "Configs");
        public static string ConfigFilePath => Path.Combine(ConfigsDirectory, "config.json");
        //public static string? OutputFilePath => MergeDirectories.GetOutputFolder(App.Config ?? ConfigManager.DefaultConfig());
    }
}
