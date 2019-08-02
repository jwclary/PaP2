using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews2
{
    class CardGame
    {
        private static Random rnd = new Random();
        public Card[] Hand { get; set; }
        public int CardsInHand { get; set; }
        public int points { get; set; }
        public string Name;
        public List<Card> Cards { get; set; }
        public static int pointer = 0;
        //JSON info
        public List<int> PointsPerGame = new List<int>();
        public int pointsTotal = 0;
        public List<string> CardHands = new List<string>();
        public int totalGames = 0;


        public CardGame()
        {
            Hand = new Card[13];
            CardsInHand = 0;
            points = 0;
        }


        public static Card[] CreateDeck()
        {
            Card[] deck = new Card[52];
            int counter = 0;

            for (int suite = 1; suite < 5; suite++)
            {
                for (int value = 1; value < 14; value++)
                {
                    deck[counter] = new Card(value, suite);
                    counter++;
                }
            }
            return deck;
        }

        public static void DrawCard(Card[] deck, ref CardGame player)
        {
            Card nextCard = deck[pointer];

            player.Hand[player.CardsInHand] = nextCard;
            if (player.CardsInHand < 13)
            {
                player.CardsInHand++;
            }
            player.points += nextCard.Points;
            pointer++;

        }

        public static void Shuffle(ref Card[] deck)
        {
            Card temp;
            int num;

            for (int i = 0; i < deck.Length; i++)
            {
                num = rnd.Next(0, deck.Length);


                temp = deck[i];
                deck[i] = deck[num];
                deck[num] = temp;
            }
        }
    }
}
