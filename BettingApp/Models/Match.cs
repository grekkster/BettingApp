using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BettingApp.Models
{
    public class Odds
    {
        public decimal Home { get; set; }
        public decimal Draw { get; set; }
        public decimal Away { get; set; }
    }

    public class Bets
    {
        public bool Home { get; set; }
        public bool Draw { get; set; }
        public bool Away { get; set; }
    }

    public class Match
    {
        public int ID { get; set; }
        public string TeamHome { get; set; }
        public string TeamAway { get; set; }
        public Odds Odds { get; set; }
        public Bets Bets { get; set; }
    }
}