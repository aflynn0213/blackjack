using BlackjackGame.Models;

namespace BlackjackGame.GameLogic
{
    public interface IPlayer
    {
        string Name { get; set; }
        Hand Hand { get; }
        bool IsBust { get; }
        int Total { get; }
        void Hit (Card card);
        void Play(Deck deck);
    }
}