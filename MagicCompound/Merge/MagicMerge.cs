using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Formats.Gif;
using MagicCompound.Configs;
using MagicCompound.Data;
using MagicCompound.Extensions;
using MagicCompound.Utils;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

namespace MagicCompound.Merge
{
    internal static class MagicMerge
    {
        internal static string? Target;

        public static void MergeImage(string baseImagePath)
        {
            Target = baseImagePath;
            string configPath = MergeDirectories.GetConfigFile(App.Args);
            App.Config = ConfigManager.LoadConfig(configPath);

            if (App.Config.OutputFolder == "%OutputFolder%" && !Path.Exists(AppDirectories.OutputDirectory))
            {
                Directory.CreateDirectory(AppDirectories.OutputDirectory);
            }

            if (ImageUtil.IsImageFile(baseImagePath))
            {
                using Image baseImage = Image.Load(baseImagePath);

                if (!ImageUtil.IsAnimatedImage(baseImage)) // Обробка статичних зображень (PNG, JPG, WEBP)
                    baseImage.Mutate(context => Merge(context, App.Config, baseImage)); 
                else baseImage.MergeAnimatedImage(App.Config); // Обробка анімованих зображень (GIF, APNG, WEBP)

                baseImage.SaveImage(App.Config);
            }
        }

        private static void Merge(IImageProcessingContext context, Config config, Image baseImage)
        {
            if (config.Layers == null)
            {
                throw new ArgumentNullException(nameof(config), "Config layers cannot be null.");
            }

            foreach (var layer in config.Layers)
            {
                if (layer.Asset != null)
                {
                    string assetPath = Path.Combine(MergeDirectories.GetAssetsFolder(config), layer.Asset);
                    
                    if (ImageUtil.IsImageFile(assetPath))
                    {
                        using Image asset = Image.Load(assetPath);
                        var position = layer.GetAdjustedPosition(baseImage.Width, baseImage.Height, asset.Width, asset.Height);
                        asset.StretchImage(baseImage, layer);
                        context.DrawImage(asset, position, layer.Opacity);
                    }
                }
            }
        }

        private static void MergeAnimatedImage(this Image baseImage, Config config)
        {
            for (int i = 0; i < baseImage.Frames.Count; i++)
            {
                int delay = baseImage.Frames[i].Metadata.GetGifMetadata().FrameDelay;
                //int frameIndex = baseImage.Frames.IndexOf(frame);
                using var frameImage = baseImage.Frames.CloneFrame(i);
                frameImage.Mutate(context => Merge(context, config, frameImage));
                frameImage.Frames.RootFrame.Metadata.GetGifMetadata().FrameDelay = delay;

                baseImage.Frames.RemoveFrame(i);
                baseImage.Frames.InsertFrame(i, frameImage.Frames.RootFrame);
            }
        }

        private static void StretchImage(this Image asset, Image baseImage, LayerInfo layer)
        {
            switch (layer.Stretch.ToCapital())
            {
                case "None":
                    break;
                case "Fill":
                    asset.Mutate(o => o.Resize(baseImage.Width, baseImage.Height));
                    break;
                case "Uniform":
                    int widthUniform = baseImage.Width;
                    int heightUniform = (int)(baseImage.Width * asset.Height / (double)asset.Width);

                    if (heightUniform > baseImage.Height)
                    {
                        heightUniform = baseImage.Height;
                        widthUniform = (int)(baseImage.Height * asset.Width / (double)asset.Height);
                    }

                    asset.Mutate(ctx => ctx.Resize(widthUniform, heightUniform)); // Зберігає пропорції
                    break;
                case "%Auto%":
                    int widthUniformToFill = baseImage.Width;
                    int heightUniformToFill = (int)(baseImage.Width * asset.Height / (double)asset.Width);

                    if (heightUniformToFill < baseImage.Height)
                    {
                        heightUniformToFill = baseImage.Height;
                        widthUniformToFill = (int)(baseImage.Height * asset.Width / (double)asset.Height);
                    }

                    asset.Mutate(ctx => ctx.Resize(widthUniformToFill, heightUniformToFill).Crop(new Rectangle(0, 0, baseImage.Width, baseImage.Height))); // Обрізає зайві частини
                    break;
                default:
                    break;
            }
        }

        private static void SaveImage(this Image baseImage, Config? config = null, string? saveImagePath = null)
        {
            string outputFolderPath = MergeDirectories.GetOutputFolder(config) ?? AppDirectories.OutputDirectory;
            string outputName = MergeDirectories.GetFileName(config, saveImagePath) ?? "image";
            string outputFormat = MergeDirectories.GetOutputFormat(baseImage, config);
            baseImage.SaveAsync(ImageUtil.GetUniqueFilePath(saveImagePath ?? outputFolderPath, outputName, outputFormat), 
                GetEncoder(outputFormat.ToLower()));
        }

        private static IImageEncoder GetEncoder(string format)
        {
            return format.ToLower() switch
            {
                "png" => new PngEncoder() { ColorType = PngColorType.RgbWithAlpha },
                "jpg" => new JpegEncoder(),
                "webp" => new WebpEncoder(),
                "gif" => new GifEncoder() { ColorTableMode = GifColorTableMode.Global, Quantizer = new OctreeQuantizer(new QuantizerOptions { Dither = null })},
                _ => new PngEncoder() // За замовчуванням PNG
            };
        }
    }
}
