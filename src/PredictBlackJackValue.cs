using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using demo_cards_functions_v2.Models;
using System.Collections.Generic;
using System.Linq;
using demo_cards_functions_v2.Helpers;

namespace demo_cards_functions_v2
{
    public static class PredictBlackJackValue
    {
        [FunctionName("PredictBlackJackValue")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            // Use LogWarning to make it easy to pinpoint to a new request in the logs (local or in-portal)
            log.LogWarning($"C# HTTP trigger function '{context.FunctionName}': received a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            PredictionResult predictionResult = JsonConvert.DeserializeObject<PredictionResult>(requestBody);

            List<Prediction> probablePredictions = predictionResult.Predictions.Where(p => p.Probability >= Constants.MinimumProbability)
                                                                  .ToList();

            if (probablePredictions != null)
            {
                List<Card> cards = new List<Card>();

                foreach (Prediction p in probablePredictions)
                {
                    string color = p.TagName.Split('_')[0];
                    string cardValue = p.TagName.Split('_')[1];

                    // Do not add lower probability predictions of a card that is already there.
                    if (!cards.Any(c => c.Tag.Equals(p.TagName)))
                    {
                        cards.Add(new Card
                        {
                            Tag = p.TagName,
                            Probability = p.Probability,
                            Value = CardHelper.GetCardValue(cardValue),
                            IsAce = cardValue.Equals("ace") ? true : false
                        }
                        );
                    }

                }

                // Calculate total blackjack value
                BlackJackValueResult result = GetCardResult(cards);

                // Use LogWarning to make it easy to pinpoint to a new request in the logs (local or in-portal)
                log.LogWarning($"C# HTTP trigger function '{context.FunctionName}': Found {cards.Count} cards with value range {result.MinValue}-{result.MaxValue}.");

                return (ActionResult)new OkObjectResult(result);
            }
            else
            {
                return (ActionResult)new NotFoundObjectResult("No cards recognized, sorry");
            }
        }

        private static BlackJackValueResult GetCardResult(List<Card> cards)
        {
            int minValue = 0;
            int maxValue = 0;
            int cardIndex = 1;

            Card card1 = new Card { Tag = "none" };
            Card card2 = new Card { Tag = "none" }; ;
            Card card3 = new Card { Tag = "none" }; ;
            Card card4 = new Card { Tag = "none" }; ;
            Card card5 = new Card { Tag = "none" }; ;
            Card card6 = new Card { Tag = "none" }; ;

            foreach (Card c in cards)
            {
                if (c.IsAce)
                {
                    minValue += 1;
                    maxValue += 11;
                }
                else
                {
                    minValue += c.Value;
                    maxValue += c.Value;
                }

                switch (cardIndex++)
                {
                    case 1:
                        card1 = c;
                        break;
                    case 2:
                        card2 = c;
                        break;
                    case 3:
                        card3 = c;
                        break;
                    case 4:
                        card4 = c;
                        break;
                    case 5:
                        card5 = c;
                        break;
                    case 6:
                        card6 = c;
                        break;
                }
            }

            return new BlackJackValueResult { 
                MinValue = minValue, 
                MaxValue = maxValue, 
                Card1 = card1, 
                Card2 = card2, 
                Card3 = card3, 
                Card4 = card4, 
                Card5 = card5, 
                Card6 = card6 
                };
        }
    }
}

