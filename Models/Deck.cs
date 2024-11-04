using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackGame.Models
{
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            InitializeDeck();
            Shuffle();
        }

        private void InitializeDeck()
        {
            cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(suit, rank));
                }
            }
        }

        public void Shuffle()
        {
            Random rng = new Random();
            cards = cards.OrderBy(card => rng.Next()).ToList();
        }

        public Card Deal()
        {
            if (cards.Count == 0) throw new InvalidOperationException("The deck is empty!");
            Card dealtCard = cards[0];
            cards.RemoveAt(0);
            return dealtCard;
        }
    }
}
