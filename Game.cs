using System;
using Card;
using System.Collections.Generic;
using System.Diagnostics;



namespace Game
{
    public class Game
    {
        private Dealer _dealer;        
        private Player[] _players;
        private Deck_Cards _deck;
        public string game_string = "Player#{0}, what is your choice?\r\n1) Check\r\n2) Hit\r\n3) Double Down\r\n";
        
        public Game(Dealer dealer,Player[] players,Deck_Cards deck)
        {
            _dealer = dealer;
            _players = players;
            _deck = deck; 
         

            Game_Loop_Start();

        }

        private void Game_Loop_Start()
        {
            Player_Loop();
            automate_dealer();
            compare_results();
        }


        private void Player_Loop()
        {
            bool inner;
            int i = 0;

            while(i < _players.Length)
            {
                //int j=1;
                inner = true;
                while(inner)
                {
                    //j++;
                    //implement code to only ask player i
                    //if cards are of equal value then show split prompt as well
                    string str = game_string + split_prompt(i);
                    Console.WriteLine(str,i+1);
                    int opt = Convert.ToInt32(Console.ReadLine());

                    switch(opt)
                    {
                        case 1:
                            Console.WriteLine("Player#{0} checks.  Next Player's Turn", i+1);
                            Console.WriteLine("Player#{0} total is {1}",i+1,_players[i].get_total());
                            inner = false;
                            break;
                        case 2:
                            _deck.Get_Card(_players,i);
                             Console.WriteLine("Player#{1} total is: {0}",_players[i].get_total(),i);

                            if (_players[i].get_total() >= 21)
                            {  
                                if(_players[i].Has_ace)
                                {
                                    Console.WriteLine("Congrats, Player#{0} has a BLACKJACK",i);
                                }
                                else
                                {
                                    Console.WriteLine(" Player#{0} has busted",i);
                                }
                                inner = false;
                            }

                            break;
                        case 3:
                            Double_Down(i);
                            break;
                        case 4:
                            //split(_players,i);
                            break;
                        default:
                            break;
                    }
                }
                i++;
            }
        }

        private string split_prompt(int i)
        {
            if (_players[i].cards[0].Val == _players[i].cards[1].Val)
            {
                return "4) Split";
            }
            return "";
        }

        private void automate_dealer() 
        {
            while(_dealer.Card_sum !> 17  || (_dealer.Card_sum==17 && _dealer.Has_ace==true))
            {
                _deck.Get_Card(_dealer);
                Console.Write("Dealer's Total is: {0}",_dealer.Card_sum);
            }
            
            Console.WriteLine("Dealer's total is {0}",_dealer.Card_sum);

        }
        
        private void compare_results()
        {
            foreach (Player p in _players)
            {
                if ((p.get_total() !> 21) && (p.get_total() > _dealer.Card_sum || _dealer.Card_sum > 21)) 
                {
                    p.Beat_dealer = true;    
                }
            }
        }
        private void Double_Down(int i)
        {
            List<Play_Card> temp = new List<Play_Card>();
            temp.Add(new Play_Card(){Val = _players[i].cards[1].Val, Type = _players[i].cards[1].Type});
            _players[i].cards.RemoveAt(1);
            Console.WriteLine("You've chosen to double down");
            _deck.Get_Card(_players,i);
            
            bool decision = false;
            
            while (!decision) 
            { 
                //j++;
                //implement code to only ask player i
                //if cards are of equal value then show split prompt as well
                string str = game_string + split_prompt(i);
                Console.WriteLine(str,i+1);
                int opt = Convert.ToInt32(Console.ReadLine());

                switch(opt)
                {
                    case 1:
                        Console.WriteLine("Player#{0} checks.  Next Player's Turn", i+1);
                        Console.WriteLine("Player#{0} total is {1}",i+1,_players[i].get_total());
                        decision = true;
                        break;
                    case 2:
                        _deck.Get_Card(_players,i);
                            Console.WriteLine("Player#{1} total is: {0}",_players[i].get_total(),i);

                        if (_players[i].get_total() >= 21)
                        {  
                            if(_players[i].Has_ace)
                            {
                                Console.WriteLine("Congrats, Player#{0} has a BLACKJACK",i);
                            }
                            else
                            {
                                Console.WriteLine(" Player#{0} has busted",i);
                            }
                            decision = true;
                        }

                        break;
                    case 3:
                        Double_Down(i);
                        break;
                    case 4:
                        //split(_players,i);
                        break;
                    default:
                        break;
                }
            }
            
            //Console.WriteLine("Player#{0} Card{1} is {2}",i,j,_players[i].cards[j].Val);
            Console.WriteLine("Player#{0} sum is {1}",i,_players[i].get_total());        
        }
        private void split(Player[] _players,int i)
        {
            List<Play_Card> temp = new List<Play_Card>();
            temp.Add(_players[i].cards[1]);
            _players[i].cards.RemoveAt(1);
            
        }
    }
}

////Have a lot to implement plus git