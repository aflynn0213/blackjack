using System;
using System.Collections.Generic;
using BlackjackGame.Models;

namespace BlackjackGame.GameLogic
{
    public class BlackjackGame
    {
        private Deck deck;
        private List<IPlayer> players;
        private Dealer dealer;
        private int currentPlayerIndex;

        public BlackjackGame(int manualPlayers, int aiPlayers)
        {
            deck = new Deck();
            players = new List<IPlayer>();

            // Add manual players
            for (int i = 0; i < manualPlayers; i++)
            {
                players.Add(new Player { Name = $"Player {i + 1}" });
            }

            // Add AI players
            for (int i = 0; i < aiPlayers; i++)
            {
                players.Add(new AIPlayer { Name = $"AI Player {i + 1}" });
            }

            dealer = new Dealer();
        }

        public void StartGame()
        {
            DealInitialCards();
            Console.WriteLine("Game started!");
            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name} Hand: {player.Hand}");
            }
            Console.WriteLine($"Dealer's visible card: {dealer.Hand.Cards[1]}");
            currentPlayerIndex = 0;
            PlayNextPlayerTurn();
        }

        private void DealInitialCards()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (var player in players)
                {
                    player.Hand.AddCard(deck.Deal());
                }
                dealer.Hand.AddCard(deck.Deal());
            }
        }

        private void PlayNextPlayerTurn()
        {
            if (currentPlayerIndex < players.Count)
            {
                var currentPlayer = players[currentPlayerIndex];
                Console.WriteLine($"{currentPlayer.Name}'s turn. Hand: {currentPlayer.Hand}");

                if (currentPlayer is AIPlayer aiPlayer)
                {
                    // AI player plays automatically
                    aiPlayer.Play(deck);
                    NextPlayerOrDealerTurn();
                }
                else
                {
                    // Manual player makes choices
                    CheckPlayerTurn(currentPlayer);
                }
            }
            else
            {
                DealerTurn();
            }
        }

        private void PlayerHits()
        {
            var currentPlayer = players[currentPlayerIndex];
            currentPlayer.Hit(deck.Deal());

            if (currentPlayer.Hand.IsBust)
            {
                Console.WriteLine($"{currentPlayer.Name} busts!");
                NextPlayerOrDealerTurn();
            }
            else
            {
                CheckPlayerTurn(currentPlayer);
            }
        }

        private void NextPlayerOrDealerTurn()
        {
            currentPlayerIndex++;
            PlayNextPlayerTurn();
        }

        private void CheckPlayerTurn(IPlayer player)
        {
            Console.WriteLine($"{player.Name}, choose: 'hit' or 'stand'");
            string action = Console.ReadLine();
            if (action == "hit")
                PlayerHits();
            else if (action == "stand")
                NextPlayerOrDealerTurn();
            else
                CheckPlayerTurn(player); // Reprompt if invalid input
        }

        private void DealerTurn()
        {
            Console.WriteLine("Dealer's turn.");
            while (dealer.Total < 17 || (dealer.Total == 17 && dealer.Hand.ContainsSoftAce()))
            {
                dealer.Hand.AddCard(deck.Deal());
                Console.WriteLine($"Dealer hits: {dealer.Hand}");
            }
            Console.WriteLine($"Dealer stands with a total of {dealer.Total}.");
            EndGame();
        }

        private void EndGame()
        {
            int dealerTotal = dealer.Total;
            Console.WriteLine($"Dealer's final hand: {dealer.Hand}, total: {dealerTotal}");

            foreach (var player in players)
            {
                int playerTotal = player.Total;
                Console.WriteLine($"{player.Name}'s final hand: {player.Hand}, total: {playerTotal}");

                if (playerTotal > 21)
                {
                    Console.WriteLine($"{player.Name} busts! Dealer wins.");
                }
                else if (dealerTotal > 21 || playerTotal > dealerTotal)
                {
                    Console.WriteLine($"{player.Name} wins against the dealer!");
                }
                else if (playerTotal == dealerTotal)
                {
                    Console.WriteLine($"{player.Name} pushes with the dealer.");
                }
                else
                {
                    Console.WriteLine($"Dealer wins against {player.Name}.");
                }
            }
        }
    }
}
