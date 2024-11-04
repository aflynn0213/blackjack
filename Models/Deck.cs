using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackGame.Models
{
    public class Deck
    {
        private static Deck _instance; // The single instance
        private static readonly object _lock = new object(); // To ensure thread safety
        private List<Card> cards;

        // Private constructor to prevent instantiation
        private Deck()
        {
            InitializeDeck();
            Shuffle();
        }

        // Public property to access the singleton instance
        public static Deck Instance
        {
            get
            {
                lock (_lock) // Ensures thread safety for multi-threaded access
                {
                    return _instance ??= new Deck();
                }
            }
        }

        // Initializes the deck with four standard decks
        private void InitializeDeck()
        {
            cards = new List<Card>();
            int numberOfDecks = 4;

            for (int i = 0; i < numberOfDecks; i++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                    {
                        cards.Add(new Card(suit, rank));
                    }
                }
            }
        }

        // Shuffle the deck
        public void Shuffle()
        {
            Random rng = new Random();
            cards = cards.OrderBy(card => rng.Next()).ToList();
        }

        // Deal a card from the deck
        public Card Deal()
        {
            if (cards.Count == 0) throw new InvalidOperationException("The deck is empty!");
            Card dealtCard = cards[0];
            cards.RemoveAt(0);
            return dealtCard;
        }

        // Property to get the number of remaining cards
        public int RemainingCards => cards.Count;
    }
}
