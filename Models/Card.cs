using System;
using System.Collections.Generic;
using System.Text;

namespace demo_cards_functions_v2.Models
{
    class Card
    {
        public string Tag { get; set; }
        public double Probability { get; set; }
        public int Value { get; set; }
        public bool IsAce { get; set; }
    }
}
