using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace demo_cards_functions_v2.Models
{
    class PredictionResult
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "project")]
        public string project { get; set; }

        [JsonProperty(PropertyName = "iteration")]
        public string iteration { get; set; }

        [JsonProperty(PropertyName = "created")]
        public DateTime created { get; set; }

        [JsonProperty(PropertyName = "predictions")]
        public List<Prediction> Predictions { get; set; }
    }
}
