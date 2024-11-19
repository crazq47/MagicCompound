using Newtonsoft.Json;
using MagicCompound.Data;

namespace MagicCompound.Configs
{
    public class Config
    {
        public string? OutputName { get; set; }
        public string[]? OutputFormat { get; set; }
        public string? OutputFolder { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? AssetsFolder { get; set; }

        public List<LayerInfo>? Layers { get; set; }
    }
}
