using System;

using BlackjackGame.Models;

namespace BlackjackGame.GameLogic
{   
    public class AIPlayer : IPlayer
    {
        public string Name { get; set; }
        public Hand Hand { get; private set; }

        public AIPlayer()
        {
            Hand = new Hand();
        }

        public void Play(Deck deck)
        {
            while (Hand.CalculateTotal() < 15)  // Example AI strategy
            {
                Hit(deck.Deal());  // AI player hits using the Hit method
            }
            Console.WriteLine($"{Name} (AI) stands with a total of {Hand.CalculateTotal()}");
        }

        public void Hit(Card card)
        {
            Hand.AddCard(card);
            Console.WriteLine($"{Name} (AI) hits and receives: {card}");
        }

        public bool IsBust => Hand.IsBust;
        public int Total => Hand.CalculateTotal();
    }


}
