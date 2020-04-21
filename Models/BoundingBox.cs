using Newtonsoft.Json;

namespace demo_cards_functions_v2.Models
{
    class BoundingBox
    {
        [JsonProperty(PropertyName = "left")]
        public double Left { get; set; }

        [JsonProperty(PropertyName = "top")]
        public double Top { get; set; }

        [JsonProperty(PropertyName = "width")]
        public double Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public double Height { get; set; }
    }
}
