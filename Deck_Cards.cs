using System;
using Card;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
    //Manages Deck(All 4) for the Game 
    public class Deck_Cards
    {
        
        //public static SortedList<Card_Type,int> deck = new SortedList<string, int>()
        //{{Card_Type.AceLow,}

        private uint[] deck_count = new uint[13] {4,4,4,4,4,4,4,4,4,4,4,4,4};

         public int generate_rand()
        {
            Random r = new Random();
            int rand = r.Next(1,13);
            return rand;
        }
        
      /*public void Get_Card(List<Play_Card> temp, int j)
        {
            int n = generate_rand();
            switch(n)  
            {
                case 1:
                    deck_count[n-1]--;
                    temp.Add(new Play_Card(){ Val = 10, Type = "Ace"});
                    players[j].Has_ace = true;
                    players[j].Card_sum = 11;
                    break;

                case 11: 
                    deck_count[n-1]--;
                    players[j].cards.Add(new Play_Card(){Val = 10, Type = "Jack"});
                    players[j].Card_sum = 10;
                    break;

                case 12:
                    deck_count[n-1]--;
                    players[j].cards.Add(new Play_Card(){Val = 10, Type = "Queen"});
                    players[j].Card_sum = 10;
                    break;

                case 13: 
                    deck_count[n-1]--;
                    players[j].cards.Add(new Play_Card(){Val = 10, Type = "King"});
                    players[j].Card_sum = 10;
                    break;

                default:
                    string keyInd = Enum.GetName(typeof(Card_Type),n);
                    deck_count[n-1]--;
                    players[j].cards.Add(new Play_Card(){Val = n, Type = keyInd});
                    players[j].Card_sum = n;
                    break;
            }
        }*/

        public void Get_Card(Player[] players, int j)
        {
            int n = generate_rand();
           
            switch(n)  
            {
                case 1:
                    deck_count[n-1]--;
                    players[j].cards.Add(new Play_Card(){ Val = 11, Type = "Ace"});
                    players[j].Has_ace = true;
                    players[j].Card_sum(11);
                    break;

                case 11: 
                    deck_count[n-1]--;
                    players[j].cards.Add(new Play_Card(){Val = 10, Type = "Jack"});
                    players[j].Card_sum(10);
                    break;

                case 12:
                    deck_count[n-1]--;
                    players[j].cards.Add(new Play_Card(){Val = 10, Type = "Queen"});
                    players[j].Card_sum(10);
                    break;

                case 13: 
                    deck_count[n-1]--;
                    players[j].cards.Add(new Play_Card(){Val = 10, Type = "King"});
                    players[j].Card_sum(10);
                    break;

                default:
                    var keyInd = Enum.GetName(typeof(Card_Type),n);
                    deck_count[n-1]--;
                    players[j].cards.Add(new Play_Card(){Val = n, Type = keyInd});
                    players[j].Card_sum(n);
                    break;
            }
        }

        public void Get_Card(Dealer dealer)
        {
            
            int n = generate_rand();
            switch(n)  
            {
                case 1:
                    deck_count[n-1]--;
                    dealer.Has_ace = true;
                    dealer.cards.Add(new Play_Card(){ Val = 11, Type = "Ace"});
                    dealer.Card_sum = 11;
                    break;

                case 11: 
                    deck_count[n-1]--;
                    dealer.cards.Add(new Play_Card(){ Val = 10, Type = "Jack"});
                    dealer.Card_sum = 10;
                    break;

                case 12:
                    deck_count[n-1]--;
                    dealer.cards.Add(new Play_Card(){ Val = 10, Type = "Queen"});
                    dealer.Card_sum = 10;
                    break;

                case 13: 
                    deck_count[n-1]--;
                    dealer.cards.Add(new Play_Card(){ Val = 10, Type = "King"});
                    dealer.Card_sum = 10;
                    break;

                default:
                    var keyInd = Enum.GetName(typeof(Card_Type),n);
                    deck_count[n-1]--;
                    dealer.cards.Add(new Play_Card(){ Val = n, Type = keyInd});
                    dealer.Card_sum = n;
                    break;
            }
        }
        public void Shuffle_decks()
        {
            for (int i = 0; i < 13; i++)
            {   
                deck_count[i] = 4;
            }
        }

        
    }

}