using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    class Hand
    {
        ArrayList cards;
        public Hand()
        {
            cards = new ArrayList();
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public int Total()
        {
            int total = 0;
            foreach (Card card in cards)
            {
                total += card.Value;
            }
            return total;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Card card in cards)
            {
                sb.Append(card + " ");
            }
            return sb.ToString();
        }
    }
}
