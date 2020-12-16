using System;
using System.Collections.Generic;
using System.Text;

namespace TradingCardGame.Model.GameModels
{
    public class Game
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public int Turn { get; set; }
    }
}
