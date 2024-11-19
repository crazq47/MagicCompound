using SixLabors.ImageSharp;
using MagicCompound.Extensions;

namespace MagicCompound.Data
{
    public class LayerInfo
    {
        public int Index { get; set; }
        public string? Asset { get; set; }
        public string Stretch { get; set; } = "none";
        public Point Position { get; set; } = new Point(0, 0);
        public float Opacity { get; set; } = 1.0f;
        public string HorizontalAlignment { get; set; } = "left";
        public string VerticalAlignment { get; set; } = "top";

        public Point GetAdjustedPosition(int baseWidth, int baseHeight, int assetWidth, int assetHeight)
        {
            int x = Position.X;
            int y = Position.Y;

            switch (HorizontalAlignment.ToCapital())
            {
                case "%Auto%":
                    x = Position.X;
                    break;
                case "Left":
                    x = Position.X;
                    break;
                case "Center":
                    x = (baseWidth - assetWidth) / 2 + Position.X;
                    break;
                case "Right":
                    x = baseWidth - assetWidth - Position.X;
                    break;
            }

            switch (VerticalAlignment.ToCapital())
            {
                case "%Auto%":
                    y = Position.Y;
                    break;
                case "Top":
                    y = Position.Y;
                    break;
                case "Center":
                    y = (baseHeight - assetHeight) / 2 + Position.Y;
                    break;
                case "Bottom":
                    y = baseHeight - assetHeight - Position.Y;
                    break;
            }

            return new Point(x, y);
        }
    }
}
