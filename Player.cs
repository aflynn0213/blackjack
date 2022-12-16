using Card;

namespace Game
{
    public class Player
    {
        //private double currency
        public List<Play_Card> cards = new List<Play_Card>();
        private bool beat_dealer = false;
        
        private bool has_ace = false;
        private int card_sum = 0;

        public bool Has_ace { get => has_ace; set => has_ace = value; }
        private bool has_bj;
        
        public void Card_sum(int n) 
        { 
            if (this.card_sum + n > 21 && this.Has_ace == true)
            {
                card_sum = card_sum + 1;
                this.Has_ace = false;
            }
            else
            {
                this.card_sum = this.card_sum + n;
                
                if(this.card_sum == 21)
                {
                    this.Has_bj = true;        
                } 
            }
        }   
        
        public int get_total() => this.card_sum;

        public bool Beat_dealer 
        { 
            get
            {
                return beat_dealer; 
            }
            set 
            {
                beat_dealer = value; 
            }
        }

        public bool Has_bj { get => has_bj; set => has_bj = value; }

        //public int Split_index { get => Split_index; set => Split_index = value; }


        public void insurance()
        {Console.Write("Implement");}


    }
}

