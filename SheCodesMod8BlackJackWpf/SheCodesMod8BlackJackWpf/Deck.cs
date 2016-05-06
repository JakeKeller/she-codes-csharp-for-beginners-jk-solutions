using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheCodesMod8BlackJackWpf
{
    /// <summary>
    /// Handles creation of a full deck of valid cards as well as the act of drawing a random card.
    /// </summary>
    public class Deck
    {
        public Card[] Cards { get; set; }
        public Random RandomGenerator { get; set; }
        public int drawnCardsCounter { get; set; }

        /// <summary>
        /// Constructor method which initializes the deck so all 52 cards are formatted to valid suits. 
        /// It also handles the process of assigning an image identifier to each card in the deck.
        /// </summary>
        public Deck()
        {
            this.drawnCardsCounter = 0;
            this.Cards = new Card[52];
            /*
            At this point all cards (objects) in the above this.Cards[] array are null. They have to be instantiated before their 
            properties can be set to a value.
            */
            int j = 0;
            foreach (var validSuit in Card.ValidSuits())
            {
                foreach (var validRank in Card.ValidRanks())
                {
                    this.Cards[j] = new Card(validSuit, validRank);
                    j++;
                }
            }
            /*
            The following code takes care of "reading in" the cards that are in folder SheCodesMod8BlackJackWpf\SheCodesMod8BlackJackWpf\Resources\classic-cards.
            52 Cards are read in in sets of 13 (each suit has 13 cards). I figured out/adjusted the order the cards in the deck are instantiated (above) and 
            wrote a for loop that assigns the images to the cards so they match. The image identifier is stored in a property of the card class.
            */
            int firstImage = 52;
            int countDown = 0;
            int offset = 0;

            for (int i = 0; i < 52; i++)
            {
                if (countDown == 13)
                {
                    firstImage --;
                    offset = 0;
                    countDown = 0;
                }
                Cards[i].ImageIdentifier = (firstImage - offset);
                offset += 4;
                countDown++;
            }
                RandomGenerator = new Random();
        }
        /// <summary>
        /// Draws a random card from the deck and sets that index of the deck to null so the card cannot be drawn again.
        /// </summary>
        /// <returns></returns>
        public Card DrawCard()
        {
            int randomNumber;
            do
                randomNumber = RandomGenerator.Next(0, 52);
            while ((this.Cards[randomNumber] == null) && (this.drawnCardsCounter < 53));

            Card drawnCard = this.Cards[randomNumber];
            this.Cards[randomNumber] = null;

            this.drawnCardsCounter++;

            return drawnCard;
        }
    }
}

