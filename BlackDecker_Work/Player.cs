using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BlackJack {
    class Player {
        private Hand hand;

        public Player() {
            hand = new Hand();
        }
        public List<string> GetDecksOfCards(List<Card> listShuffleDeck, int dealCount, List<string> listDeckCard)
        {
            for (int i = 0; i < dealCount; i++)
            {
                var shuffleCard = listShuffleDeck.FirstOrDefault();
                listShuffleDeck.Remove(shuffleCard);
                listDeckCard.Add(shuffleCard.SuitValue);
            }
            return listDeckCard;
        }
        public override string ToString(){
            return hand.ToString();
        }
    }

}
