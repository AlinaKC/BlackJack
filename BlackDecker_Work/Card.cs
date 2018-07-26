namespace BlackJack
{
    class Card
    {
        public int Value { get; }
        public Suit Suit { get; set; }
        public string SuitValue { get; set; }
        public Card(Suit suit, string suitValue)
        {
            Suit = suit;
            SuitValue = suitValue;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
