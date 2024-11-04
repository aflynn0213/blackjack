using System;
using System.Collections.Generic;

using BlackjackGame.Models;

namespace BlackjackGame.GameLogic
{
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public List<Hand> Hands { get; set; } = new List<Hand>(); // Store multiple hands for splitting
        
        private Hand _currentHand;
        public Hand CurrentHand
        {
            get => _currentHand ??= Hands[0];
            set => _currentHand = value;
        }

        public bool HasStood { get; set; } = false;

        public Player()
        {
            // Initialize with one default hand
            Hands.Add(new Hand());
            _currentHand = Hands[0];
        }

        public void Hit(Card card)
        {
            CurrentHand.AddCard(card);
            Console.WriteLine($"{Name} hits and receives: {card}");
        }

        public void DoubleDown(Card card)
        {
            Console.WriteLine($"{Name} has doubled down!");
            Hit(card); // Double down should hit and end turn
        }

        public bool IsBust => CurrentHand.IsBust;
        public int Total => CurrentHand.CalculateTotal();

        public virtual void Play(Deck deck) {}

        public override string ToString() => $"{Name}'s Hand: {CurrentHand}";
    }
}
