using System;

using BlackjackGame.Models;

namespace BlackjackGame.GameLogic
{
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public Hand Hand { get; private set; }

        public Player()
        {
            Hand = new Hand();
        }

        public virtual void Play(Deck deck)
        {
            // Manual players don’t have automatic actions
        }

        public void Hit(Card card)
        {
            Hand.AddCard(card);
            Console.WriteLine($"{Name} hits and receives: {card}");
        }

        public bool IsBust => Hand.IsBust;
        public int Total => Hand.CalculateTotal();

        public override string ToString() => $"{Name}'s Hand: {Hand}";
    }

}