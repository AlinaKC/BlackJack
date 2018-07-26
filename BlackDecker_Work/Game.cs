using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    class Game
    {
        Deck deck;
        Player player;
        Dealer dealer;
        List<Card> listShuffleDeck;
        List<string> listPlayerCard;
        List<string> listHouseCard;
        string houseCard = string.Empty;
        string playerCard = string.Empty;
        string cardList = string.Empty;
        int aceValue = 0;
        int playerTotal = 0;
        int houseTotal = 0;
        bool playing = true;

        public Game()
        {
            deck = new Deck();
            player = new Player();
            dealer = new Dealer();
        }

        public void DeclarationReset()
        {
            houseCard = string.Empty;
            playerCard = string.Empty;
            cardList = string.Empty;
            aceValue = 0;
            playerTotal = 0;
            houseTotal = 0;
            playing = true;
        }

        public void DealInitialCards()
        {
            DeclarationReset();
            Deck deckShuffle = new Deck();
            listShuffleDeck = deckShuffle.CardShuffleList();
            listPlayerCard = new List<string>();
            listHouseCard = new List<string>();

            dealer.GetDecksOfCards(listShuffleDeck, 2, listHouseCard);
            Console.WriteLine("House: *,{0}", listHouseCard[1].ToString());

            player.GetDecksOfCards(listShuffleDeck, 2, listPlayerCard);
            Console.WriteLine("Player: {0},{1} ", listPlayerCard[0].ToString(), listPlayerCard[1].ToString());

            while (playing)
            {
                Console.Write("What would you like to do: deal, hit, stand, quit?: ");
                string response = Console.ReadLine();
                switch (response.ToUpper())
                {
                    case "HIT":
                        Hit();
                        break;
                    case "STAND":
                        Stand();
                        break;
                    case "QUIT":
                        Environment.Exit(-1);
                        break;
                    case "DEAL":
                        Console.Clear();
                        DealInitialCards();
                        break;
                    default:
                        Console.WriteLine("Wrong Command. Try again");
                        break;
                }
            }
            BusterResult(listHouseCard, listPlayerCard);
        }

        public void Hit()
        {
            houseCard = ConvertCardToString(listHouseCard);
            playerCard = ConvertCardToString(listPlayerCard);
            player.GetDecksOfCards(listShuffleDeck, 1, listPlayerCard);
            string cardList = ConvertCardToString(listPlayerCard);
            playerTotal = GetTotal(listPlayerCard);
            GetAceValue(listPlayerCard, playerTotal);
            playing = !IsBusted(playerTotal);
            if (playing)
            {
                Console.WriteLine("House: *,{0}", listHouseCard[1].ToString());
                Console.WriteLine("Player: {0}", cardList);
            }
        }

        public void Stand()
        {
            houseTotal = GetTotal(listHouseCard);
            List<string> listExceptFirstIndex = new List<string>();
            while (houseTotal <= 21)
            {
                listExceptFirstIndex = new List<string>();
                playerTotal = GetTotal(listPlayerCard);
                GetAceValue(listPlayerCard, playerTotal);
                playerTotal = GetTotal(listPlayerCard);

                cardList = ConvertCardToString(listPlayerCard);
                GetAceValue(listPlayerCard, houseTotal);
                if (houseTotal <= 17 || houseTotal < playerTotal)
                {

                    listExceptFirstIndex = new List<string>();
                    dealer.GetDecksOfCards(listShuffleDeck, 1, listHouseCard);
                    string dealerCardList = ConvertCardToString(listHouseCard);

                    GetAceValue(listHouseCard, houseTotal);
                    houseTotal = GetTotal(listHouseCard);
                    playing = !IsBusted(houseTotal);
                    if (playing)
                    {
                        listExceptFirstIndex = listHouseCard;
                        Console.WriteLine("House: *,{0}", ConvertCardToString(listExceptFirstIndex.Skip(1).ToList()));
                        Console.WriteLine("Player: {0}", cardList);
                    }
                }
                else
                    playing = false;
                houseTotal = GetTotal(listHouseCard);
                playing = false;

                if (houseTotal >= playerTotal)
                    BusterResult(listHouseCard, listPlayerCard);
            }
        }

        public void BusterResult(List<string> listHouseCard, List<string> listPlayerCard)
        {

            Console.WriteLine("House: {0}", ConvertCardToString(listHouseCard));
            Console.WriteLine("Player: {0}", ConvertCardToString(listPlayerCard));

            houseTotal = GetTotal(listHouseCard);
            playerTotal = GetTotal(listPlayerCard);

            if (IsBusted(playerTotal))
            {
                Console.WriteLine("You lost the game.");
                DealerQuit();
            }
            else
            {
                if (IsBusted(houseTotal))
                {
                    Console.WriteLine("You won the game.");
                    DealerQuit();
                }
                else
                {
                    Console.WriteLine();
                    if (playerTotal > houseTotal)
                    {
                        Console.WriteLine("You won the game.");
                        DealerQuit();
                    }
                    else if (playerTotal < houseTotal)
                    {
                        Console.WriteLine("You lost the game.");
                        DealerQuit();
                    }
                    else
                    {
                        Console.WriteLine("Game is tie.");
                        DealerQuit();
                    }
                }
            }
        }

        public void DealerQuit()
        {
            bool isInvalid = true;
            while (isInvalid)
            {
                Console.Write("What would you like to do: deal, quit?: ");
                string response = Console.ReadLine();
                switch (response.ToUpper())
                {
                    case "DEAL":
                        Console.Clear();
                        DealInitialCards();
                        isInvalid = false;
                        break;
                    case "QUIT":
                        Environment.Exit(-1);
                        isInvalid = false;
                        break;
                    default:
                        Console.WriteLine("Wrong Command. Try again");
                        break;
                }
            }
        }

        public int GetTotal(List<string> value)
        {
            int total = 0;
            foreach (var val in value)
            {
                switch (val)
                {
                    case "A":
                        total = total + aceValue;
                        break;
                    case "J":
                        total = total + 10;
                        break;
                    case "Q":
                        total = total + 10;
                        break;
                    case "K":
                        total = total + 10;
                        break;
                    default:
                        total = total + Convert.ToInt32(val);
                        break;

                }
            }
            return total;
        }

        public void GetAceValue(List<string> listCards, int deckTotal)
        {
            aceValue = 1;
            if (listCards.Count == 2)
            {
                if (listCards[0] == "A" && listCards[1] == "A")
                    aceValue = 12;

                else if (listCards.Contains("A"))
                    if (deckTotal <= 10)
                        aceValue = 11;
                    else
                        aceValue = 1;
            }
            else
            {
                if (deckTotal <= 10)
                    aceValue = 11;
            }
        }

        public string ConvertCardToString(List<string> cardList)
        {
            string cardString = "";
            foreach (var cardObj in cardList)
            {
                if (cardString == "")
                    cardString = cardObj;
                else
                    cardString = cardString + ", " + cardObj;
            }
            return cardString;
        }

        public bool IsBusted(int total)
        {
            if (total > 21)
                return true;
            return false;
        }
    }
}
