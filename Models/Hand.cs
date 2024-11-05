using System.Collections.Generic;
using System.Linq;

namespace BlackjackGame.Models
{
    public class Hand
    {
        public List<Card> Cards { get; private set; } = new List<Card>();

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public int CalculateTotal()
        {
            int total = Cards.Sum(card => card.Value);
            int aceCount = Cards.Count(card => card.Rank == Rank.Ace);

            // Adjust Aces from 11 to 1 if total > 21
            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }

        public bool ContainsSoftAce()
        {
            int total = Cards.Sum(card => card.Value);
            int aceCount = Cards.Count(card => card.Rank == Rank.Ace);

            // Adjust for soft ace scenario
            return aceCount > 0 && total <= 21 && total + 10 > 21;
        }

        public bool IsBust => CalculateTotal() > 21;

        public int Total => CalculateTotal();

        public override string ToString() => string.Join(", ", Cards);
    }
}