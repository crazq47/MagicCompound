using System.Windows;
using System.IO;
using MagicCompound.Configs;
using MagicCompound.Merge;

namespace MagicCompound
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static Config? Config;
        internal static string[]? Args;

        public App()
        {
            this.Startup += OnStartup;
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            Args = e.Args;
            string configFile = MergeDirectories.GetConfigFile(Args);
            
            if (!File.Exists(configFile))
                Config = ConfigManager.ResetConfig(configFile);
            else Parameters.GetArguments(Args);

            this.Shutdown();
        }
    }
}
