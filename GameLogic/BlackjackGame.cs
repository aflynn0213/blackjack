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
            deck = Deck.Instance;
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
                Console.WriteLine($"{player.Name}'s Hand: {player.CurrentHand} for a total: {player.CurrentHand.Total}");
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
                    player.CurrentHand.AddCard(deck.Deal());
                }
                dealer.Hand.AddCard(deck.Deal());
            }
        }

        private void PlayNextPlayerTurn()
        {
            if (currentPlayerIndex < players.Count)
            {
                var currentPlayer = players[currentPlayerIndex];
                Console.WriteLine($"{currentPlayer.Name}'s turn. Hand: {currentPlayer.CurrentHand} for a total:{currentPlayer.CurrentHand.Total}");

                if (currentPlayer is AIPlayer aiPlayer)
                {
                    aiPlayer.Play(deck);
                    NextPlayerOrDealerTurn();
                }
                else
                {
                    CheckPlayerTurn(currentPlayer);
                }
            }
            else
            {
                DealerTurn();
            }
        }

        private void PlayerHits(IPlayer player)
        {
            player.Hit(deck.Deal());
            if (player.CurrentHand.IsBust)
            { 
                Console.WriteLine($"{player.Name} busts!");
                if (!player.midSplit) 
                {
                    NextPlayerOrDealerTurn();
                }
                else
                {
                    return;
                }
            }
            else
            {
                CheckPlayerTurn(player);
            }
        }

        private void NextPlayerOrDealerTurn()
        {
            currentPlayerIndex++;
            PlayNextPlayerTurn();
        }

        private void CheckPlayerTurn(IPlayer player)
        {
            Console.Write($"{player.Name}, choose: '1) hit' or '2) stand' ");
            bool canDoubleDown = player.CurrentHand.Cards.Count == 2;
            bool canSplit = player.CurrentHand.Cards.Count == 2 && player.CurrentHand.Cards[0].Rank == player.CurrentHand.Cards[1].Rank;

            if (canDoubleDown) Console.Write(" or '3) double down'");
            if (canSplit) Console.Write(" or '4) split'");

            Console.WriteLine();
            string action = Console.ReadLine()?.Trim().ToLower();

            switch (action)
            {
                case "1":
                    PlayerHits(player);
                    break;
                case "2":
                    Console.WriteLine($"{player.Name}'s final hand: {player.CurrentHand}");
                    if (player.midSplit)
                    {
                        player.HasStood = true;
                        return;
                    }
                    else
                    {
                        NextPlayerOrDealerTurn();
                    }
                    break;
                case "3" when canDoubleDown:
                    player.DoubleDown(deck.Deal());
                    if (player.midSplit)
                    {
                        player.HasStood = true;
                        return;
                    }
                    else
                    {
                        NextPlayerOrDealerTurn();
                    }
                    break;
                case "4" when canSplit:
                    player.SplitHand(deck);
                    PlaySplitHands(player);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    CheckPlayerTurn(player);
                    break;
            }
        }

        private void PlaySplitHands(IPlayer player)
        {
            Console.WriteLine($"{player.Name} is now playing split hands.");

            foreach (var hand in player.Hands)
            {
                player.CurrentHand = hand;
                Console.WriteLine($"Playing hand: {hand}");

                while (!hand.IsBust && !player.HasStood)
                {
                    CheckPlayerTurn(player);
                }
                player.HasStood = false; // Reset for next hand
            }

            NextPlayerOrDealerTurn();
        }

        private void DealerTurn()
        {
            Console.WriteLine("Dealer's turn.");
            while (dealer.Total < 17 || (dealer.Total == 17 && dealer.Hand.ContainsSoftAce()))
            {
                dealer.Hand.AddCard(deck.Deal());
                Console.WriteLine($"Dealer hits: {dealer.Hand}");
            }
            Console.WriteLine($"Dealer total is {dealer.Total}.");

            EndGame();
        }

        private void CleanOutHands()
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].Hands.Clear(); // Clear all hands for the current player
                players[i].Hands.Add(new Hand()); // Add a new, empty hand
                players[i].CurrentHand = players[i].Hands[0];
            }
            dealer.Hand.Cards.Clear();
        }

        private void EndGame()
        {
            int dealerTotal = dealer.Total;
            Console.WriteLine($"Dealer's final hand: {dealer.Hand}, total: {dealerTotal}");

            foreach (var player in players)
            {
                int handNumber = 1;
                foreach(Hand hand in player.Hands)
                {
                    int playerTotal = hand.Total;
                    Console.WriteLine($"{player.Name}'s hand #{handNumber}: {hand}, total: {playerTotal}");

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

                    handNumber +=1;
                }
            }

            Console.WriteLine("Would you like to play again? (y/n)");
            string action;
            do
            {
                action = Console.ReadLine()?.Trim().ToLower();
                if (action == "n")
                {
                    Console.WriteLine("Thank you for playing!");
                    return;
                }
                else if (action == "y")
                {
                    CleanOutHands();
                    StartGame();
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'y' to play again or 'n' to exit.");
                }
            } while (action != "y" && action != "n");
        }
    }
}
