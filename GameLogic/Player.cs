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
        public bool midSplit =>  Hands[Hands.Count-1] != CurrentHand;

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
            Console.WriteLine($"{Name}'s Hand: {CurrentHand} for a total: {CurrentHand.Total} ");
        }

        public void DoubleDown(Card card)
        {
            Console.WriteLine($"{Name} has doubled down!");
            Hit(card); // Double down should hit and end turn
        }

        public void SplitHand(Deck deck)
        {
            Console.WriteLine($"{Name} chose to split!");

            int currentHandIndex = Hands.IndexOf(CurrentHand);

            // Create two new hands with the cards from the current hand
            var firstHand = new Hand();
            var secondHand = new Hand();

            // Copy one card to each new hand
            firstHand.AddCard(CurrentHand.Cards[0]);
            secondHand.AddCard(CurrentHand.Cards[1]);

            // Deal one new card to each split hand
            firstHand.AddCard(deck.Deal());
            secondHand.AddCard(deck.Deal());

            // Replace the current hand with the first hand and insert the second hand next
            Hands[currentHandIndex] = firstHand;
            Hands.Insert(currentHandIndex + 1, secondHand);

            Console.WriteLine($"{Name} now has {Hands.Count} hands.");
        }

        public bool IsBust => CurrentHand.IsBust;

        public virtual void Play(Deck deck) {}
        public override string ToString() => $"{Name}'s Hand: {CurrentHand}";
    }
}
