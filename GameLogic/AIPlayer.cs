using System;
using System.Collections.Generic;
using BlackjackGame.Models;

namespace BlackjackGame.GameLogic
{
    public class AIPlayer : IPlayer
    {
        public string Name { get; set; }
        public List<Hand> Hands { get; set; } = new List<Hand>();
        
        private Hand _currentHand;
        public Hand CurrentHand
        {
            get => _currentHand ??= Hands[0];
            set => _currentHand = value; // Allow setting a different hand as CurrentHand
        }  

        public bool HasStood { get; set; } = false;

        public AIPlayer()
        {
            // Initialize with one default hand
            Hands.Add(new Hand());
            _currentHand = Hands[0];
        }

        public void Hit(Card card)
        {
            CurrentHand.AddCard(card);
            Console.WriteLine($"{Name} (AI) hits and receives: {card}");
        }

        public void DoubleDown(Card card)
        {
            Console.WriteLine($"{Name} (AI) doubles down!");
            Hit(card); 
        }

        public bool IsBust => CurrentHand.IsBust;
        public int Total => CurrentHand.CalculateTotal();

        public void Play(Deck deck)
        {
            // AI decision logic
            while (Total < 17)
            {
                Hit(deck.Deal());
            }
            HasStood = true;
            Console.WriteLine($"{Name} (AI) stands with total: {Total}");
        }

        public override string ToString() => $"{Name}'s Hand: {CurrentHand}";
    }
}
