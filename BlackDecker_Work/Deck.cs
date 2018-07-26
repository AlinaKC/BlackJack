using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    class Deck
    {
        public List<Card> Cards { get; set; }
        public Deck()
        {
            Cards = new List<Card>();
            List<string> values = Enumerable
                .Range(2, 9)
                .Select(x => x.ToString())
                .Concat(new[] { "A", "J", "Q", "K" })
                .ToList();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (string value in values)
                {
                    Cards.Add(new Card(suit, value));
                }
            }
        }
        public List<Card> CardShuffleList()
        {
            Deck cardDeck = new Deck();
            List<Card> shuffleDeckCards = new List<Card>();
            Random randomCards = new Random();
            int cardIndex = 0;
            while (cardDeck.Cards.Count > 0)
            {
                cardIndex = randomCards.Next(0, cardDeck.Cards.Count);
                shuffleDeckCards.Add(cardDeck.Cards[cardIndex]);
                cardDeck.Cards.Remove(cardDeck.Cards[cardIndex]);
            }
            return shuffleDeckCards;
        }
    }
}
