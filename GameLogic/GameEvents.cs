using System;

namespace BlackjackGame.GameLogic
{
    public class GameEvents
    {
        public event Action PlayerHit;
        public event Action PlayerStand;
        public event Action DealerTurn;
        public event Action GameOver;

        public void OnPlayerHit() => PlayerHit?.Invoke();
        public void OnPlayerStand() => PlayerStand?.Invoke();
        public void OnDealerTurn() => DealerTurn?.Invoke();
        public void OnGameOver() => GameOver?.Invoke();
    }
}