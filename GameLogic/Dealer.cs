using System;
using System.Collections.Generic;

using BlackjackGame.Models;


namespace BlackjackGame.GameLogic
{
    public class Dealer
    {
        public Hand Hand { get; private set; }
        public int Total => Hand.CalculateTotal();

        public Dealer()
        {
            Hand = new Hand();
        }

        public void AddCard(Card card)
        {
            Hand.AddCard(card);
        }

        public bool HasAceShowing => Hand.Cards.Count > 1 && Hand.Cards[1].Rank == Rank.Ace;

        public void PlayTurn(Deck deck)
        {
            // Continue hitting until reaching a "hard 17" or higher
            while (Total < 17 || (Total == 17 && Hand.ContainsSoftAce()))
            {
                AddCard(deck.Deal());
                Console.WriteLine($"Dealer hits: {Hand}");
            }
            Console.WriteLine($"Dealer stands with a total of {Total}");
        }

        public void OfferInsurance()
        {
            if (HasAceShowing)
            {
                Console.WriteLine("Dealer shows an Ace. Offering insurance to players.");
            }
        }
    }
}

