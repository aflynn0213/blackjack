namespace Card
{
    public struct Play_Card 
    {
        private const string Message = "Not a valid value";
        private int val;
        private string type;

        public int Val 
        {
            get => val; 
            
            set
            {
                if (value>=1 && value<=11)
                {
                    val = value;
                } 
                else {
                    System.Exception exception = new System.Exception(Message);
                    throw exception; 
                }
            }
        }

        public string Type {get => type; set=>type=value;}

    }
    public enum Card_Type
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
    }
}