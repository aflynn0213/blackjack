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
                Console.WriteLine($"{player.Name} Hand: {player.CurrentHand}");
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
                Console.WriteLine($"{currentPlayer.Name}'s turn. Hand: {currentPlayer.CurrentHand}");

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

            if (currentPlayer.CurrentHand.IsBust)
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
            // Show available options based on current hand
            Console.Write($"{player.Name}, choose: '1) hit' or '2) stand'");

            bool canDoubleDown = player.CurrentHand.Cards.Count == 2;
            bool canSplit = player.CurrentHand.Cards.Count == 2 && player.CurrentHand.Cards[0].Rank == player.CurrentHand.Cards[1].Rank;

            if (canDoubleDown)
            {
                Console.Write(" or '3) double down'");
            }

            if (canSplit)
            {
                Console.Write(" or '4) split'");
            }

            Console.WriteLine();
            string action = Console.ReadLine();

            // Perform the action based on input
            if (action == "hit")
                PlayerHits();
            else if (action == "stand")
                NextPlayerOrDealerTurn();
            else if (action == "double down" && canDoubleDown)
                PlayerDoublesDown(player);
            else if (action == "split" && canSplit)
                PlayerSplits(player);
            else
            {
                Console.WriteLine("Invalid choice. Please type 'hit', 'stand', 'double down', or 'split' if available.");
                CheckPlayerTurn(player); // Re-prompt if invalid input
            }
        }

        private void PlayerDoublesDown(IPlayer player)
        {
            Console.WriteLine($"{player.Name} chose to double down!");
            
            // Assume `double` is just adding one more card
            player.DoubleDown(deck.Deal()); // You may need a method to track the bet as well
            Console.WriteLine($"{player.Name} final hand after double down: {player.CurrentHand}");

            if (player.IsBust)
            {
                Console.WriteLine($"{player.Name} busts on double down!");
            }

            NextPlayerOrDealerTurn(); // End turn after double down
        }

        private void PlayerSplits(IPlayer player)
        {
            Console.WriteLine($"{player.Name} chose to split!");

            // Split the hand into two separate hands
            var firstHand = new Hand();
            var secondHand = new Hand();

            // Assign one card to each hand
            firstHand.AddCard(player.CurrentHand.Cards[0]);
            secondHand.AddCard(player.CurrentHand.Cards[1]);

            // Add one card to each new hand from the deck
            firstHand.AddCard(deck.Deal());
            secondHand.AddCard(deck.Deal());

            // Replace the original hand with the first hand and save the second hand
            player.Hands = new List<Hand> { firstHand, secondHand };

            // Play both hands as separate turns
            PlaySplitHands(player);
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
            foreach (var player in players)
            {
                player.Hands.Clear();
                player.CurrentHand = new Hand();
                player.Hands.Add(player.CurrentHand);
            }

            dealer.Hand.Cards.Clear();
        }
        private void EndGame()
        {
            int dealerTotal = dealer.Total;
            Console.WriteLine($"Dealer's final hand: {dealer.Hand}, total: {dealerTotal}");

            foreach (var player in players)
            {
                int playerTotal = player.Total;
                Console.WriteLine($"{player.Name}'s final hand: {player.CurrentHand}, total: {playerTotal}");

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

            Console.WriteLine("Would you like to play again? (y/n)");
            string action;
            do
            {
                action = Console.ReadLine()?.Trim().ToLower();
                if (action == "n")
                {
                    Console.WriteLine("Thank you for playing!");
                    return; // Exit the game loop entirely
                }
                else if (action == "y")
                {
                    CleanOutHands();
                    StartGame(); // Restart the game
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'y' to play again or 'n' to exit.");
                }
            } while (action != "y" && action != "n");
        }
    }
}
