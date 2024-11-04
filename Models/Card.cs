namespace BlackjackGame.Models
{
    public class Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }
        public int Value => GetCardValue();

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        private int GetCardValue() => Rank switch
        {
            Rank.Ace => 11,
            Rank.King or Rank.Queen or Rank.Jack => 10,
            _ => (int)Rank
        };

        public override string ToString() => $"{Rank} of {Suit}";
    }
}
