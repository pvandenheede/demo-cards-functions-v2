using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace demo_cards_functions_v2.Models
{
    public class PredictionResult
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "project")]
        public string Project { get; set; }

        [JsonProperty(PropertyName = "iteration")]
        public string Iteration { get; set; }

        [JsonProperty(PropertyName = "created")]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "predictions")]
        public List<Prediction> Predictions { get; set; }
    }
}
