using System.IO;
using Microsoft.Win32;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using MagicCompound.Merge;

namespace MagicCompound.Utils
{
    public class ImageUtil
    {        
        public static void GetImages(string[]? args)
        {
            if (args != null && args.Length > 0)
                MergeDroppedImages(args);
            else ShowImagesDialog();
        }

        public static void MergeDroppedImages(string[] arg)
        {
            foreach (string image in arg)
            {
                MagicMerge.MergeImage(image);
            }
        }

        public static void ShowImagesDialog()
        {
            foreach (string image in OpenImagesDialog())
            {
                MagicMerge.MergeImage(image);
            }
        }

        public static string[] OpenImagesDialog()
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Image Files|*.png;*.jpg;*.gif;*.webp",
                Multiselect = true,
                Title = "Select Images"
            };

            bool? result = openFileDialog.ShowDialog();
            return result == true ? openFileDialog.FileNames : [];
        }

        public static string GetUniqueFilePath(string directory, string fileName, string? fileExtension = null)
        {
            string extension = (fileExtension ?? Path.GetExtension(fileName)).TrimStart('.');
            string baseFileName = Path.GetFileNameWithoutExtension(fileName);

            int uniqueIndex = GetUniqueIndex(directory, baseFileName, extension);

            string indexedFileName = uniqueIndex == 0
                ? $"{baseFileName}.{extension}"
                : $"{baseFileName}({uniqueIndex}).{extension}";

            return Path.Combine(directory, indexedFileName);
        }

        public static string GetUniqueFileName(string directory, string fileName, string? fileExtension = null)
        {
            string extension = (fileExtension ?? Path.GetExtension(fileName)).TrimStart('.');
            string baseFileName = Path.GetFileNameWithoutExtension(fileName);

            int uniqueIndex = GetUniqueIndex(directory, baseFileName, extension);

            return uniqueIndex == 0
                ? baseFileName
                : $"{baseFileName}({uniqueIndex})";
        }

        public static int GetUniqueIndex(string directory, string? baseFileName = null, string? extension = null)
        {
            baseFileName ??= Path.GetFileNameWithoutExtension(directory);
            extension ??= Path.GetExtension(directory).TrimStart('.');

            int counter = 0;
            string filePath;

            do
            {
                string indexedFileName = counter == 0
                    ? $"{baseFileName}.{extension}"
                    : $"{baseFileName}({counter}).{extension}";

                filePath = Path.Combine(directory, indexedFileName);
                counter++;
            }
            while (File.Exists(filePath));

            return counter - 1;
        }

        public static bool IsImageFile(string filePath)
        {
            try
            {
                using var stream = File.OpenRead(filePath);
                IImageFormat format = Image.DetectFormat(stream);
                return format != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsAnimatedImage(Image targetImage)
        {
            return targetImage.Frames.Count > 1;
        }

        public static bool IsAnimatedImage(string filePath)
        {
            try
            {
                using var stream = File.OpenRead(filePath);
                return IsAnimatedImage(Image.Load(stream)); // Перевіряємо кількість кадрів
            }
            catch (Exception)
            {
                return false; // Якщо файл не є зображенням або сталася помилка
            }
        }
    }
}
