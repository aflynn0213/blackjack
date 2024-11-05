using System;

namespace BlackjackGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Blackjack!");

            int manualPlayers = 0;
            int aiPlayers = 0;

            while (true)
            {
                // Get the number of manual players
                Console.WriteLine("Enter the number of manual players (1 to 5): ");
                string manualInput = Console.ReadLine();
                
                if (!int.TryParse(manualInput, out manualPlayers) || manualPlayers < 1 || manualPlayers > 5)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                    continue;
                }

                // Get the number of AI players
                Console.WriteLine("Enter the number of AI players (0 to 5): ");
                string aiInput = Console.ReadLine();
                
                if (!int.TryParse(aiInput, out aiPlayers) || aiPlayers < 0 || aiPlayers > 5)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 0 and 5.");
                    continue;
                }

                // Check if the total number of players is within the allowed range
                if (manualPlayers + aiPlayers > 5)
                {
                    Console.WriteLine("The total number of players cannot exceed 5. Please try again.");
                    continue;
                }
                
                // Ensure there is at least one manual player
                if (manualPlayers < 1)
                {
                    Console.WriteLine("There must be at least one manual player. Please try again.");
                    continue;
                }

                break; // If all validations pass, break out of the loop
            }

            Console.WriteLine($"Starting a game with {manualPlayers} manual player(s) and {aiPlayers} AI player(s)...");

            var game = new BlackjackGame.GameLogic.BlackjackGame(manualPlayers, aiPlayers);
            game.StartGame();
        }
    }
}
