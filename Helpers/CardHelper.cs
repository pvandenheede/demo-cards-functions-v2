using System;
using System.Collections.Generic;
using System.Text;

namespace demo_cards_functions_v2.Helpers
{
    public static class CardHelper
    {
        public static int GetCardValue(string tag)
        {
            switch (tag)
            {
                case "ace":     return 11;
                case "king":    return 10;
                case "queen":   return 10;
                case "jack":    return 10;
                case "ten":     return 10;
                case "nine":    return 9;
                case "eight":   return 8;
                case "seven":   return 7;
                case "six":     return 6;
                case "five":    return 5;
                case "four":    return 4;
                case "three":   return 3;
                case "two":     return 2;
                
                default:        throw new ArgumentException($"Unknown tag '{tag}'. Cannot evaluate to card value.");
            }
        }
    }
}
