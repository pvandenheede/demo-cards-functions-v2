using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using demo_cards_functions_v2.Models;
using demo_cards_functions_v2.Helpers;

namespace demo_cards_functions_v2
{
    public static class GuessSingleCard
    {
        

        [FunctionName("GuessSingleCard")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            // Use LogWarning to make it easy to pinpoint to a new request in the logs (local or in-portal)
            log.LogWarning($"C# HTTP trigger function '{context.FunctionName}': received a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            PredictionResult predictionResult = JsonConvert.DeserializeObject<PredictionResult>(requestBody);

            Prediction highestScoringPrediction = predictionResult.Predictions.Where(pr => pr.Probability >= Constants.MinimumProbability)
                                    .OrderByDescending(pr => pr.Probability)
                                    .FirstOrDefault();

            if (highestScoringPrediction != null)
            {

                var dynResult = new { 
                        Tag = highestScoringPrediction.TagName,
                        Probability = highestScoringPrediction.Probability, 
                        Value = CardHelper.GetCardValue(highestScoringPrediction.TagName.Split('_')[1]) 
                        };
                var jsonToReturn = JsonConvert.SerializeObject(dynResult);

                // Use LogWarning to make it easy to pinpoint to a new request in the logs (local or in-portal)
                log.LogWarning($"C# HTTP trigger function '{context.FunctionName}': Found tag '{dynResult.Tag}' with value '{dynResult.Value}' and probability of '{dynResult.Probability}'.");
                
                return (ActionResult)new OkObjectResult(dynResult);
            }
            else
            {
                return (ActionResult)new NotFoundObjectResult("Card not recognized, sorry");
            }
        }


    }
}
