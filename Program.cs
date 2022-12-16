using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;
using System.Timers;

namespace Game
{
    class Program
    {   
        private static void initialize_game(Player[] players,Dealer _dealer,Deck_Cards deck,int num_players)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < num_players+1; j++)
                {
                    if (j < num_players)
                    {
                        if (i==0)
                        {
                            Player playa = new Player();
                            players[j] =  playa;
                        }
                        
                        deck.Get_Card(players,j);
                        Console.WriteLine("Player#{0} Card#{1} is a {2}", j+1, i+1, players[j].cards[i].Type);
                       
                    }
                    else 
                    {
                        deck.Get_Card(_dealer);
                        
                        if (i==0)
                        {
                            Console.WriteLine("Dealer dealt his first card face down");
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Dealer's Card#{0} is a {1}", i+1, _dealer.cards[i].Type);    
                        }
                    }

                        
                }
            }
        }
        static void Main()
        {
            var _dealer = new Dealer();
            var deck = new Deck_Cards();
           
            deck.Shuffle_decks();
            Console.WriteLine("How many players are there?");
            var num = Console.ReadLine();
            var num_players = Convert.ToInt32(num);
            Player[] players = new Player[num_players];

            initialize_game(players,_dealer,deck,num_players); 

            Game game = new Game(_dealer,players,deck);
            
        }
    }
}

