using Card;

namespace Game
{
    public class Dealer
    {
        public List<Play_Card> cards = new List<Play_Card>();
        private bool has_ace = false;
        public bool Has_ace { get => has_ace; set => has_ace = value; }
     
        private int card_sum = 0;

        public int Card_sum 
        { 
            get
            {
                return card_sum;
            }
            set
            {
                if (card_sum + value > 21 && this.Has_ace == true)
                {
                    card_sum = card_sum + 1;
                    this.Has_ace = false;
                }
                else
                {
                    card_sum = card_sum + value; 
                }
            }
        }
        public void insurance()
        {
            Console.Write("Implement");
        }
    }
}

