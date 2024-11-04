using System.Collections.Generic;
using BlackjackGame.Models;

namespace BlackjackGame.GameLogic
{
    public interface IPlayer
    {
        string Name { get; set; }
        List<Hand> Hands { get; set; } // To support split hands
        Hand CurrentHand { get; set;} // The active hand

        bool HasStood { get; set; }
        bool IsBust { get; }
        int Total { get; }

        void Hit(Card card);
        void DoubleDown(Card card);
        void Play(Deck deck); // Method for turn logic
    }
}
