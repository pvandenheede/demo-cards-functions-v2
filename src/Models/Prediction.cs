using Newtonsoft.Json;

namespace demo_cards_functions_v2.Models
{
    public class Prediction
    {
        [JsonProperty(PropertyName = "probability")]
        public double Probability { get; set; }

        [JsonProperty(PropertyName = "tagId")]
        public string TagId { get; set; }

        [JsonProperty(PropertyName = "tagName")]
        public string TagName { get; set; }

        [JsonProperty(PropertyName = "boundingBox")]
        public BoundingBox BoundingBox { get; set; }
    }
}
