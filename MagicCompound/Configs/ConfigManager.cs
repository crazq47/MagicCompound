using System.IO;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using MagicCompound.Data;
using MagicCompound.Merge;

namespace MagicCompound.Configs
{
    static class ConfigManager
    {
        private static string? _baseDirectory;
        private static bool _loadState;
        private static bool _saveState;

        public static string BaseDirectory => _baseDirectory ?? AppDirectories.ConfigFilePath;

        public static Config LoadConfig(string? directory = null)
        {
            _baseDirectory = directory;
            _loadState = true;

            if (!File.Exists(BaseDirectory))
                return ResetConfig();

            string json = File.ReadAllText(BaseDirectory);
            return JsonConvert.DeserializeObject<Config>(json) ?? new Config();
        }

        public static void SaveConfig(Config config, string? directory = null)
        {
            ArgumentNullException.ThrowIfNull(config);
            _baseDirectory = directory;
            _saveState = true;

            string configsFolder = AppDirectories.ConfigsDirectory;

            if (Path.GetDirectoryName(BaseDirectory) == configsFolder)
            {
                if (!Directory.Exists(configsFolder))
                    Directory.CreateDirectory(configsFolder);
            }

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(BaseDirectory, json);
        }

        public static Config GetConfig(Config? config = null)
        {
            return config ?? DefaultConfig();
        }

        public static Config UpdateConfig(this Config config, string directory)
        {
            Config configuration = LoadConfig(directory);
            return configuration ?? GetConfig(config);
        }

        public static Config ResetConfig(string? directory = null)
        {
            _baseDirectory = directory;

            if (File.Exists(BaseDirectory))
                File.Delete(BaseDirectory);

            Config config = DefaultConfig();
            SaveConfig(config);

            return config;
        }

        public static Config DefaultConfig()
        {
            return new Config
            {
                OutputName = "%Name%",
                OutputFormat = ["jpg", "gif"],
                OutputFolder = "%TargetFolder%",
                AssetsFolder = null,
                Layers = ParseLayers()
            };
        }

        public static List<LayerInfo>? ParseLayers()
        {
            string? assetsDirectory = MergeDirectories.GetAssetsFolder();

            if (!Directory.Exists(assetsDirectory))
            {
                ResetConfig();
                return null;
            }

            var assetFiles = Directory.GetFiles(assetsDirectory);
            var assetList = new List<LayerInfo>();
            var watermarkAssets = new List<LayerInfo>();

            foreach (var file in assetFiles)
            {
                var fileName = Path.GetFileName(file);
                var splitFileName = fileName.Split(MergeSyntax.Index);

                if (!fileName.StartsWith(MergeSyntax.Ignore))
                {
                    // Обробка індексів шарів
                    if (splitFileName.Length == 2 && int.TryParse(splitFileName[0], out int index))
                    {
                        assetList.Add(new LayerInfo
                        {
                            Index = index,
                            Asset = fileName
                        });
                    }
                    // Інакше якщо значення splitFileName дорівнює "watermark", додаємо в watermarkAssets
                    else if (splitFileName.Length == 2 && splitFileName[0].Equals(MergeSyntax.Watermark, StringComparison.OrdinalIgnoreCase))
                    {
                        watermarkAssets.Add(new LayerInfo
                        {
                            Index = assetList.Count != 0 ? assetList.Last().Index + 1 : 0, // Використовуємо максимальне значення для сортування в кінці
                            Asset = fileName,
                            Position = new Point(25, 20),
                            HorizontalAlignment = "right",
                            VerticalAlignment = "bottom",
                            Opacity = 0.25f
                        });
                    }
                }
            }

            // Сортуємо основні елементи за індексом і додаємо watermark-елементи в кінці
            return [.. assetList.OrderBy(a => a.Index), .. watermarkAssets];
        }

        public static bool IsLoaded()
        {
            return _loadState;
        }

        public static bool IsSaved()
        {
            return _saveState;
        }
    }
}
