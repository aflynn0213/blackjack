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

        public void SplitHand(Deck deck)
        {
            // Automated split logic for AI
            Console.WriteLine($"{Name} (AI) is splitting!");

            int currentHandIndex = Hands.IndexOf(CurrentHand);

            var firstHand = new Hand();
            var secondHand = new Hand();

            firstHand.AddCard(CurrentHand.Cards[0]);
            secondHand.AddCard(CurrentHand.Cards[1]);

            firstHand.AddCard(deck.Deal());
            secondHand.AddCard(deck.Deal());

            Hands[currentHandIndex] = firstHand;
            Hands.Insert(currentHandIndex + 1, secondHand);

            Console.WriteLine($"{Name} (AI) now has two hands.");
        }

        public bool IsBust => CurrentHand.IsBust;

        public void Play(Deck deck)
        {
            // AI decision logic
            while (CurrentHand.Total < 17)
            {
                Hit(deck.Deal());
            }
            HasStood = true;
            Console.WriteLine($"{Name} (AI) stands with total: {CurrentHand.Total}");
        }

        
        public bool midSplit =>  Hands[Hands.Count-1] != CurrentHand;
        public override string ToString() => $"{Name}'s Hand: {CurrentHand}";
    }
}
