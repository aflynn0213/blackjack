
# Blackjack Game

Welcome to the Blackjack Game! This project implements a command-line Blackjack game in C# that allows both human and AI players to play against a dealer. The game includes classic Blackjack features like hitting, standing, splitting, and doubling down, with a single shared deck across all games.

## Table of Contents

1. [Getting Started](#getting-started)
2. [Gameplay](#gameplay)
3. [Features](#features)
4. [Code Structure](#code-structure)
5. [Future Enhancements](#future-enhancements)
6. [Contributing](#contributing)

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 5.0 or later)
- Visual Studio Code or another C#-compatible editor (optional)

### Installation

1. Clone this repository:

   ```bash
   git clone https://github.com/aflynn0213/blackjack.git
   ```

2. Navigate to the project directory:

   ```bash
   cd blackjack
   ```

3. Build the project:

   ```bash
   dotnet build
   ```

4. Run the game:

   ```bash
   dotnet run
   ```

## Gameplay

The game supports up to five players (both manual and AI). Players can perform the following actions:

- **Hit**: Draw another card from the deck.
- **Stand**: End the turn with the current hand.
- **Double Down**: Double the bet, draw one final card, and end the turn.
- **Split**: Split a pair into two separate hands if the first two cards have the same rank.

The dealer plays by traditional rules, hitting until reaching a “hard 17.”

### Controls

When prompted during your turn, enter the corresponding action as follows:

- `hit`: Draw another card.
- `stand`: End your turn.
- `double down`: Double the bet and take one final card (available if your hand has only two cards).
- `split`: Split the hand into two separate hands if you have a pair (available if the first two cards are the same rank).

## Features

- **Multiple Players**: Allows up to five players, including both manual and AI-controlled.
- **Standard Blackjack Actions**: Includes hitting, standing, doubling down, and splitting.
- **Dealer Logic**: Dealer automatically hits until they reach a hard 17.
- **Singleton Deck**: A single deck is used across all game rounds.
- **Player Reset Option**: Players can choose to play again after each game.

## Code Structure

- **Program.cs**: Entry point of the application. Sets up the game and initializes players.
- **GameLogic/BlackjackGame.cs**: Contains the main game logic, handling rounds, player turns, and dealer actions.
- **Models/Deck.cs**: Implements a singleton pattern for the deck to ensure a single shared deck.
- **Models/Hand.cs**: Manages a player's or dealer's hand, calculating totals and checking for busts.
- **GameLogic/Player.cs** and **GameLogic/AIPlayer.cs**: Define player behavior, including AI strategies.
- **GameLogic/Dealer.cs**: Implements dealer-specific behavior according to Blackjack rules.
- **Interfaces/IPlayer.cs**: Defines the player interface for common actions, enabling manual and AI players to interact uniformly.

## Future Enhancements

Here are potential improvements that could be made to the game:

- **Unit Tests**: Add comprehensive unit tests to ensure the stability and accuracy of game features.
- **Graphical Interface**: Implement a GUI for a more engaging player experience.
- **Additional Game Modes**: Add variations of Blackjack, such as multiplayer mode or different betting systems.
- **Improved AI**: Enhance the AI logic for more realistic gameplay.
- **Multithreading for Concurrent Games**: Allow multiple Blackjack games to run concurrently.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with a detailed explanation of your changes.

1. Fork the Project
2. Create your feature branch (`git checkout -b feature/YourFeature`)
3. Commit your changes (`git commit -m 'Add new feature'`)
4. Push to the branch (`git push origin feature/YourFeature`)
5. Open a pull request
